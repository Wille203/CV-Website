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
                    SenderId = m.SenderId < m.ReceiverId ? m.SenderId : m.ReceiverId,
                    ReceiverId = m.SenderId < m.ReceiverId ? m.ReceiverId : m.SenderId
                })
                .Select(g => g.OrderByDescending(m => m.MessageId).FirstOrDefault())
                .ToList();

            return View(messages);
        }

        //[HttpPost]
        //public IActionResult Conversation(int senderId, int receiverId)
        //{
        //    var conversation = _context.Messages
        //        .Where(m => (m.SenderId == senderId && m.ReceiverId == receiverId) || (m.SenderId == receiverId && m.ReceiverId == senderId))
        //        .OrderBy(m => m.MessageId)
        //        .ToList();

        //    var sender = _context.Users.Find(senderId);
        //    var receiver = _context.Users.Find(receiverId);
        //    ViewBag.SenderName = sender.Name;
        //    ViewBag.ReceiverName = receiver.Name;
        //    ViewBag.ReceiverId = receiverId;
        //    ViewBag.SenderId = senderId;
        //    return View(conversation);
        //}


        [HttpGet]
        public IActionResult Conversation(int senderId, int receiverId)
        {
            //var currentUserId = CurrentUserId();

            //if (currentUserId != senderId && currentUserId != receiverId)
            //{
            //    return Unauthorized();
            //}

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
                    return RedirectToAction("Overview");
                }
            }

            TempData["ErrorMessage"] = "The message could not be sent!";
            return RedirectToAction("Overview");
        }
    }
}
