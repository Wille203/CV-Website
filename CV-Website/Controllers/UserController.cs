using CV_Website.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging.Signing;
using System.Linq;


namespace CV_Website.Controllers
{
    public class UserController : Controller
    {
        private CVContext users;

        public UserController(CVContext service)
        {
            users = service;
        }

        public IActionResult SettingsUser()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add(User user)
        {
            if (ModelState.IsValid)
            {
                users.Add(user);
                users.SaveChanges();

                //Eventuellt?
                return View(/*userConfirmation*/);
            }
            else
            {
                return View(user);

            }
        }
    }
}
