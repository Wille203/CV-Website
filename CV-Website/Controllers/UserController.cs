using CV_Website.Models;
using CV_Website.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using NuGet.Packaging.Signing;
using System.Linq;
using System.Net;



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

            if( userId != GetCurrentUserId())
            {
                if (!Request.Cookies.ContainsKey($"ViewedCV_{userId}"))
                {
                    if (userCV != null)
                    {
                        userCV.ViewCount++;
                        _context.SaveChanges();

                        Response.Cookies.Append($"ViewedCV_{userId}", "true", new Microsoft.AspNetCore.Http.CookieOptions
                        {
                            Expires = System.DateTime.Now.AddMinutes(10)
                        });
                    }
                }
            }

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


        [Authorize] 
        [HttpGet]
        public IActionResult SettingsUser(int userId)
        {

            //Primärt för NullReferenceException, vi behöver nog inte denna. 
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return NotFound();
            }
            var viewModel = new UserSettingsViewModel
            {
                Id = user.Id,
                Name = user.Name,
                UserName = user.Email,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
                Private = user.Private,
                Email = user.Email
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SettingsUser(UserSettingsViewModel updatedUser)
        {
            if (!ModelState.IsValid)
            {
                
                return View(updatedUser);
            }

            
            var user = _context.Users.FirstOrDefault(u => u.Id == updatedUser.Id);
            if (user != null)
            {
                user.UserName = updatedUser.UserName;
                user.Email = updatedUser.UserName; // ta ej bort även om det ser ut som dubble lagring för då kan vi inte logga in
                user.PhoneNumber = updatedUser.PhoneNumber;
                user.Address = updatedUser.Address;
                user.Name = updatedUser.Name;
                user.Private = updatedUser.Private;

                _context.Users.Update(user);
                _context.SaveChanges();
            }

            return RedirectToAction("GoToUserPage", new { userId = GetCurrentUserId()});
        }
        
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

        [HttpPost]
        public IActionResult SendMessageFromUser(Models.Message message)
        {
            if (ModelState.IsValid)
            {
                _context.Messages.Add(message);
                _context.SaveChanges();

                return RedirectToAction("GoToUserPage", "User", new { userId = message.ReceiverId });
            }
            var user = _context.Users.FirstOrDefault(u => u.Id == message.ReceiverId);
            var projects = _context.Project
                .Where(p => p.Users.Any(u => u.Id == message.ReceiverId))
                .ToList();
            var userCV = _context.CVs
                .Include(cv => cv.Skills)
                .Include(cv => cv.Experience)
                .Include(cv => cv.Education)
                .FirstOrDefault(cv => cv.UserId == message.ReceiverId);

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

    }
}
