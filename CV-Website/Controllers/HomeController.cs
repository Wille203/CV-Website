using CV_Website.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CV_Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CVContext _context;

        public HomeController(ILogger<HomeController> logger, CVContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var CVList = _context.CVs.ToList();

            if (CVList == null || !CVList.Any())
            {
                ViewBag.Message = "No CVs found.";
            }

            return View(CVList); 
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
