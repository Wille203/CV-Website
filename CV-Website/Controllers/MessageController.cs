using CV_Website.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using NuGet.Protocol.Plugins;

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
            //MÅSTE BYTA UT TILL RIKTIGT ID
            int currentUserId = 1;
            ViewBag.CurrentUserId = currentUserId;

            var messages = _context.Messages
                .Include(m => m.Sender)
                .Include(m => m.Receiver)
                .Where(m => m.SenderId == currentUserId || m.ReceiverId == currentUserId)
                .GroupBy(m => new
                {
                    SenderId = m.SenderId < m.ReceiverId ? m.SenderId : m.ReceiverId,
                    ReceiverId = m.SenderId < m.ReceiverId ? m.ReceiverId : m.SenderId
                })
                .Select(g => new
                {
                    LatestMessage = g.OrderByDescending(m => m.MessageId).FirstOrDefault(),
                    UnreadMessages = g.Count(m => !m.Read && m.ReceiverId == currentUserId)
                })
                .ToList();

            return View(messages);
        }

        


        [HttpGet]
        [Authorize]
        public IActionResult Conversation(int senderId, int receiverId)
        {
            //MÅSTE BYTA UT TILL RIKTIGT ID
            int currentUserId = 1; 
            ViewBag.CurrentUserId = currentUserId;

            if (currentUserId != senderId && currentUserId != receiverId)
            {
                return Unauthorized();
            }

            var conversation = _context.Messages
                .Where(m => (m.SenderId == senderId && m.ReceiverId == receiverId) || (m.SenderId == receiverId && m.ReceiverId == senderId))
                .OrderBy(m => m.MessageId)
                .ToList();

            var latestMessage = conversation.LastOrDefault();
            int? latestMessageSenderId = latestMessage?.SenderId;

            var otherUserId = currentUserId == senderId ? receiverId : senderId;
            var otherUser = _context.Users.Find(otherUserId);
            var currentUser = _context.Users.Find(currentUserId);
            ViewBag.OtherUserName = otherUser.Name;
            ViewBag.OtherUserId = otherUserId;
            ViewBag.CurrentUserName = currentUser.Name;
            ViewBag.LatestMessageSenderId = latestMessageSenderId;

            return View(conversation);
        }

        public IActionResult SendMessage(Models.Message message)
        {
            if (ModelState.IsValid)
            {
                _context.Messages.Add(message);
                _context.SaveChanges();

                if (message.SenderId.HasValue)
                {
                    return RedirectToAction("Conversation", new { senderId = message.SenderId.Value, receiverId = message.ReceiverId });
                }
                else
                {
                    TempData["ErrorMessage"] = "Sender or Receiver ID is missing.";
                }
            }

            TempData["ErrorMessage"] = "The message could not be sent!";
            return RedirectToAction("Overview");
        }

        public IActionResult MarkAsRead(int messageId)
        {
            var message = _context.Messages.Find(messageId);
            if (message != null)
            {
                message.Read = true;
                _context.SaveChanges();
            }
            return RedirectToAction("Conversation", new { senderId = message.SenderId, receiverId = message.ReceiverId });
        }

        public IActionResult DeleteMessage(int messageId)
        {
            var message = _context.Messages.Find(messageId);
            if (message != null)
            {
                _context.Messages.Remove(message);
                _context.SaveChanges();
            }
            return RedirectToAction("Conversation", new { senderId = message.SenderId, receiverId = message.ReceiverId });
        }
    }
}
