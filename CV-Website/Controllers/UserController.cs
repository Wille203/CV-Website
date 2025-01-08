using CV_Website.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using System.Linq;


namespace CV_Website.Controllers
{
    public class UserController : Controller
    {
        private CVContext _context;
        private CVContext users;
        public User CurrentUser;

        public UserController(CVContext service, CVContext context)
        {
            users = service;
            _context = context;
        }


        [HttpGet]
        public IActionResult GoToUserPage(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId); 
            if (user == null)
            {
                return NotFound(); 
            }

            
            ViewData["CurrentUser"] = user;

            return View("Userpage", user); 
        }


        //[Authorize] /*Fungerar denna?*/
        [HttpGet]
        public IActionResult SettingsUser(int userId)
        {
            // Sparar ID i den nuvarande sessionen
            var loggedInUserId = HttpContext.Session.GetString("UserId");

            if (loggedInUserId == null || loggedInUserId != userId.ToString())
            {
                return Unauthorized();
            }

            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }


        //Hämtar formuläret/vyn för användaren
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        //Läggar till ny användare i databasen, skapa en vy som heter Add
        [HttpPost]
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
