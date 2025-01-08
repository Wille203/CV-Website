using CV_Website.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CV_Website.Controllers
{
    public class AccountController : Controller
    {
        private readonly CVContext _context;

        public AccountController(CVContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Logout()
        {
            // Rensa sessionen
            HttpContext.Session.Clear();

            // Redirecta användaren till startsidan eller en annan lämplig sida
            return RedirectToAction("Index", "Home");
        }

        // GET: /Account/Register
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == model.Email);

                if (existingUser != null)
                {
                    ModelState.AddModelError("Email", "Denna e-postadress är redan registrerad.");
                    return View(model);
                }

                bool IsPrivate = model.Private;
                var user = new User
                {
                    Name = model.Name,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Address = model.Address,
                    Password = model.Password,
                    Private = IsPrivate
                };

                // Lägg till användaren och spara i databasen
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return RedirectToAction("Login");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Kontrollera om användaren finns i databasen
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == model.Email);

                if (user == null)
                {
                    ModelState.AddModelError("", "Användaren finns inte.");
                    return View(model);
                }

                // Kontrollera om lösenordet är korrekt
                if (user.Password != model.Password) // Här jämför vi lösenordet direkt utan hashning
                {
                    ModelState.AddModelError("", "Fel lösenord.");
                    return View(model);
                }

                // Logga in användaren (här kan du använda session, cookie eller annan metod för autentisering)
                HttpContext.Session.SetString("UserId", user.UserId.ToString());
                HttpContext.Session.SetString("UserName", user.Name);

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
    }
}
