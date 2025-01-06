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
            var messages = _context.Messages
                .GroupBy(m => new
                {
                    SenderId = m.SenderId < m.ReciverId ? m.SenderId : m.ReciverId,
                    ReciverId = m.SenderId < m.ReciverId ? m.ReciverId : m.SenderId
                })
                .Select(g => g.OrderByDescending(m => m.MessageId).FirstOrDefault())
                .ToList();

            return View(messages);
        }

        [HttpPost]
        public IActionResult Conversation(int senderId, int reciverId)
        {
            var conversation = _context.Messages
                .Where(m => (m.SenderId == senderId && m.ReciverId == reciverId) || (m.SenderId == reciverId && m.ReciverId == senderId))
                .OrderBy(m => m.MessageId)
                .ToList();

            var sender = _context.Users.Find(senderId);
            var reciver = _context.Users.Find(reciverId);
            ViewBag.SenderName = sender.Name;
            ViewBag.ReceiverName = reciver.Name;
            return View(conversation);
        }

        [HttpPost]
        public IActionResult SendMessage(Models.Message message)
        {
            if (ModelState.IsValid)
            {
                _context.Messages.Add(message);
                _context.SaveChanges();
                return RedirectToAction("Conversation", new { senderId = message.SenderId, receiverId = message.ReciverId });

            }
            else
            {
                ViewBag.ErrorMessage = "The message could not be sent!";
                return View(message);
            }
        }
    }
}
