using Microsoft.AspNetCore.Mvc;

namespace CV_Website.Controllers
{
    public class ProjectController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
