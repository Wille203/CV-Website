using CV_Website.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using NuGet.Protocol.Plugins;

namespace CV_Website.Controllers
{
    public class MessageController : Controller
    {
        private CVContext _context;

        public MessageController(CVContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Overview()
        {
            int currentUserId = 1;

            var messages = _context.Messages
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
        public IActionResult Conversation(int senderId, int receiverId)
        {
            //var currentUserId = CurrentUserId();
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

            var sender = _context.Users.Find(senderId);
            var receiver = _context.Users.Find(receiverId);
            ViewBag.SenderName = sender.Name;
            ViewBag.ReceiverName = receiver.Name;
            ViewBag.ReceiverId = receiverId;
            ViewBag.SenderId = senderId;
            return View(conversation);
        }

        //private int CurrentUserId()
        //{
        //    return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        //}

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
