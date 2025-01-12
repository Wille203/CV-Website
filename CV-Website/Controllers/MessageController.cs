using CV_Website.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using NuGet.Protocol.Plugins;
using System.Linq;

namespace CV_Website.Controllers
{
    public class MessageController : BaseController
    {
        private CVContext _context;

        public MessageController(CVContext context) : base(context)
        {
            _context = context;
        }


        [HttpGet]
        [Authorize]
        public IActionResult Overview()
        {
            int currentUserId = GetCurrentUserId().Value;

            var anonymousMessages = _context.Messages
                .Include(m => m.Sender)
                .Include(m => m.Receiver)
                .Where(m => m.SenderId == null && m.ReceiverId == currentUserId)
                .Select(m => new
                {
                    LatestMessage = m,
                    UnreadMessages = m.Read ? 0 : 1,
                    SenderName = m.SenderName
                })
                .ToList();

            var registeredMessages = _context.Messages
                .Include(m => m.Sender)
                .Include(m => m.Receiver)
                .Where(m => m.SenderId != null && (m.SenderId == currentUserId || m.ReceiverId == currentUserId))
                .GroupBy(m => m.SenderId == currentUserId
                    ? m.ReceiverId
                    : m.SenderId)
                .Select(g => new
                {
                    LatestMessage = g.OrderByDescending(m => m.MessageId).FirstOrDefault(),
                    SenderName = g.Key == currentUserId ? g.FirstOrDefault().Receiver.Name : g.FirstOrDefault().Sender.Name,
                    UnreadMessages = g.Count(m => !m.Read && m.ReceiverId == currentUserId)
                })
                .ToList();

            var allMessages = anonymousMessages
                .Select(m => new { m.LatestMessage, m.UnreadMessages, SenderName = m.SenderName })
                .Concat(registeredMessages.Select(m => new { m.LatestMessage, m.UnreadMessages, m.SenderName }))
                .OrderByDescending(m => m.LatestMessage.MessageId)
                .ToList();

            return View(allMessages);
        }


        [HttpGet]
        [Authorize]
        public IActionResult Conversation(int? senderId, int receiverId, string senderName = null)
        {
            int currentUserId = GetCurrentUserId().Value;

            if (currentUserId != senderId && currentUserId != receiverId)
            {
                return Unauthorized();
            }

            var conversation = _context.Messages
                .Where(m => (m.SenderId == senderId && m.ReceiverId == receiverId) ||
                            (m.SenderId == null && m.SenderName == senderName && m.ReceiverId == receiverId) ||
                            (m.SenderId == receiverId && m.ReceiverId == senderId))
                .OrderBy(m => m.MessageId)
                .ToList();

            var latestMessage = conversation.LastOrDefault();
            int? latestMessageSenderId = latestMessage?.SenderId;
            int? otherUserId = currentUserId == senderId ? receiverId : senderId;
            var otherUser = _context.Users.Find(otherUserId);

            if (otherUser == null)
            {
                ViewBag.OtherUserName = senderName;
                ViewBag.OtherUserId = null; 
            }
            else
            {
                ViewBag.OtherUserName = otherUser.Name;
                ViewBag.OtherUserId = otherUserId;
            }

            ViewBag.LatestMessageSenderId = latestMessageSenderId;

            return View(conversation);
        }

        [HttpPost]
        public IActionResult SendMessage(Models.Message message)
        {
            if (ModelState.IsValid)
            {
                _context.Messages.Add(message);
                _context.SaveChanges();

                    return RedirectToAction("Conversation", new { senderId = message.SenderId.Value, receiverId = message.ReceiverId });
            }
            var conversationMessages = _context.Messages
               .Where(m => (m.SenderId == message.SenderId && m.ReceiverId == message.ReceiverId) ||
                            (m.SenderId == message.ReceiverId && m.ReceiverId == message.SenderId))
               .OrderBy(m => m.MessageId)
               .ToList();
               ViewBag.CurrentUserId = GetCurrentUserId();
               ViewBag.OtherUserId = message.ReceiverId;
            return View("Conversation", conversationMessages);
            
        }

        [HttpPost]
        [Authorize]
        public IActionResult MarkAsRead(int messageId)
        {
            var message = _context.Messages.Find(messageId);
            if (message != null)
            {
                message.Read = true;
                _context.SaveChanges();

                if (message.SenderId == null)
                {
                    return RedirectToAction("Conversation", new { senderId = (int?)null, receiverId = message.ReceiverId, senderName = message.SenderName });
                }
                else
                {
                    return RedirectToAction("Conversation", new { senderId = message.SenderId, receiverId = message.ReceiverId });
                }
            }

            return NotFound();
        }


        [HttpPost]
        [Authorize]
        public IActionResult DeleteMessage(int messageId)
        {
            var message = _context.Messages.Find(messageId);
            if (message != null)
            {
                _context.Messages.Remove(message);
                _context.SaveChanges();
            }
            return RedirectToAction("Conversation", new { senderId = message.SenderId.Value, receiverId = message.ReceiverId });
        }
    }
}
