﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnicornInsurance.MVC.Contracts;
using UnicornInsurance.MVC.Models;

namespace UnicornInsurance.MVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly IAuthenticationService _authService;

        public UsersController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        public IActionResult Login(string returnUrl = null)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM login, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                returnUrl ??= Url.Content("~/");
                var isLoggedIn = await _authService.Authenticate(login);
                if (isLoggedIn)
                    return LocalRedirect(returnUrl);
            }
            ModelState.AddModelError("", "Log In Attempt Failed. Please try again.");
            return View(login);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registration)
        {
            if (ModelState.IsValid)
            {
                var returnUrl = Url.Content("~/");
                var isCreated = await _authService.Register(registration);
                if (isCreated)
                    return Redirect(nameof(PleaseVerifyEmail));
            }

            ModelState.AddModelError("", "Registration Attempt Failed. Please try again.");
            return View(registration);
        }

        [HttpPost]
        public async Task<IActionResult> Logout(string returnUrl)
        {
            returnUrl ??= Url.Content("~/");
            await _authService.Logout();
            return LocalRedirect(returnUrl);
        }

        public IActionResult PleaseVerifyEmail()
        {
            return View();
        }

        public async Task<IActionResult> VerifyEmail(string token)
        {
            var model = new VerifyEmailVM
            {
                VerificationSuccessful = await _authService.VerifyEmail(token)
            };

            return View(model);
        }
    }
}
