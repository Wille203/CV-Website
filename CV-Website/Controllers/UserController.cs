﻿using CV_Website.Models;
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


        //[Authorize] /*Fungerar denna?*/
        [HttpGet]
        public IActionResult SettingsUser(int userID)
        {
            return View();
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

        [HttpGet]
        public IActionResult UserPage()
        {
            return View();
        }
    }
}
