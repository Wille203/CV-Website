using CV_Website.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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


    }
}
