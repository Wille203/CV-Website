using CV_Website.Models;
using CV_Website.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using System.Linq;



namespace CV_Website.Controllers
{
    public class UserController : BaseController
    {
        private CVContext _context;

        public UserController( CVContext context) : base(context)
        {
            _context = context;
        }

       


        [HttpGet]
        public IActionResult GoToUserPage(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId); 
            if (user == null)
            {
                return NotFound(); 
            }

            var projects = _context.Project
          .Where(p => p.Users.Any(u => u.Id == userId))
          .ToList();

            //Hämtar all data samtidigt, istället för att hämta en sak åt gången
            var userCV = _context.CVs
                .Include(cv => cv.Skills)
                .Include(cv => cv.Experience)
                .Include(cv => cv.Education)
                .FirstOrDefault(cv => cv.UserId == userId);

            var viewModel = new UserPageViewModel
            {
                User = user,
                Projects = projects,
                UserCV = userCV,
                Skills = userCV?.Skills?.ToList(),
                Experiences = userCV?.Experience?.ToList(),
                Educations = userCV?.Education?.ToList()
            };

            return View("UserPage", viewModel);
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
            var loggedInUserId = HttpContext.Session.GetString("UserId");

            if (loggedInUserId == null || loggedInUserId != userId.ToString())
            {
                return Unauthorized();
            }

            //Primärt för NullReferenceException, vi behöver nog inte denna. 
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return NotFound();
            }
            var viewModel = new UserSettingsViewModel
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
                Private = user.Private
            };

            return View(viewModel);
        }

       /* [HttpPost]
        public IActionResult SettingsUser(User updatedUser)
        {
            if (!ModelState.IsValid)
            {
                
                return View(updatedUser);
            }


            var user = _context.Users.FirstOrDefault(u => u.Id == updatedUser.Id);
            if (user != null)
            {
                user.Name = updatedUser.Name;
                user.UserName = updatedUser.UserName;
                user.Address = updatedUser.Address;
                user.Private = updatedUser.Private;
                user.PhoneNumber = updatedUser.PhoneNumber;

                _context.Users.Update(user);
                _context.SaveChanges();
            }

            return RedirectToAction("GoToUserPage", new { userId = updatedUser.Id });
        }
       */ //Bort kommenterad för användningen av .password måste ändras till nya Passwordhash och hur den nu funkar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadImage(int id, IFormFile profileImage)
        {

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            if (profileImage != null && profileImage.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await profileImage.CopyToAsync(memoryStream);
                    user.img = memoryStream.ToArray(); 
                }

                _context.Update(user);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("GoToUserPage", new { userId = id });
        }

    }
}
