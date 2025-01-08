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

        public IActionResult Search(string inputstring)
        {
            if (string.IsNullOrWhiteSpace(inputstring))
            {
                return PartialView("_Partialview", new List<User>());
            }

            var users = _context.Users
                .Where(user => user.Name.Contains(inputstring))
                .ToList();

            return PartialView("_Partialview", users);
        }


        //[Authorize] denna fungerar inte atm, kör på session tills vidare
        [HttpGet]
        public IActionResult SettingsUser(int userId)
        {
            // Sparar ID i den nuvarande sessionen
            var loggedInUserId = HttpContext.Session.GetString("UserId");

            if (loggedInUserId == null || loggedInUserId != userId.ToString())
            {
                return Unauthorized();
            }

            //Primärt för NullReferenceException, vi behöver nog inte denna. 
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

    }
}
