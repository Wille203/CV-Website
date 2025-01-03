using Microsoft.AspNetCore.Mvc;

namespace CV_Website.Controllers
{
    public class UserController : Controller
    {
    
        public IActionResult Index()
        {
            return View();
        }
    }
}
