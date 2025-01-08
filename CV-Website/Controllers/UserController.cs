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

        public UserController( CVContext context)
        {
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

        public async Task<IActionResult> Search(string inputstring)
        {
            if (inputstring == null) return View(new List<User>());
            
            var users = await _context.Users
                .Where(user => user.Name.ToUpper().Contains(inputstring.ToUpper()))
                  .ToListAsync();


            
            return View(users);
        }


        //[Authorize] denna fungerar inte atm, kör på session tills vidare
        [HttpGet]
        public IActionResult SettingsUser(int userId)
        {
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

        [HttpPost]
        public IActionResult SettingsUser(User updatedUser)
        {
            if (!ModelState.IsValid)
            {
                return View(updatedUser); 
            }
            var user = _context.Users.FirstOrDefault(u => u.UserId == updatedUser.UserId);
            if (user != null)
            {
                user.Name = updatedUser.Name;
                user.Email = updatedUser.Email;
                user.Address = updatedUser.Address;
                user.Private = updatedUser.Private;
                _context.Users.Update(user);
                _context.SaveChanges();
            }

            return RedirectToAction("GoToUserPage", new { userId = updatedUser.UserId });
        }
    }
}
