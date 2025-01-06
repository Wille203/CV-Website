using CV_Website.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;


namespace CV_Website.Controllers
{
    public class UserController : Controller
    {
        private CVContext User;

        public UserController(CVContext service)
        {
            User = service;
        }

        public IActionResult SettingsUser()
        {
            return View();
        }

    }
}
