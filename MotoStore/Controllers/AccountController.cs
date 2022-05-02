﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonData;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MotoStore.MapConfigurations;
using MotoStore.Models;
using MotoStore.Services.Abstractions;
using MotoStore.ViewModels.Account;
using MotoStore.ViewModels.Layout;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace MotoStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly StoreApplicationContext _db;
        private readonly IAccountService _accountService;
        private readonly IEmailService _emailService;
        private readonly IDefaultAvatarService _defaultAvatarService;
        private readonly IWebHostEnvironment _environment;

        public AccountController(
            StoreApplicationContext db,
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            ILogger<AccountController> logger,
            IAccountService accountService, 
            IEmailService emailService, 
            IDefaultAvatarService defaultAvatarService, 
            IWebHostEnvironment environment)
        {
            _db = db;
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _accountService = accountService;
            _emailService = emailService;
            _defaultAvatarService = defaultAvatarService;
            _environment = environment;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new LayoutViewModel
            {
                RegisterViewModel = new RegisterViewModel(),
                LoginViewModel = new LoginViewModel()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Register(LayoutViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = model.RegisterViewModel.MapToUser();
                
                // string path = Path.Combine(_environment.ContentRootPath, "wwwroot\\UserFiles\\Avatars\\");
                // _uploadService.Upload(path, model.File.FileName, model.File);
                user.PathToAvatar = _defaultAvatarService.GetPathToDefaultAvatar(_environment);// string imagePath = $"UserFiles/Avatars/{model.File.FileName}";
                var result = await _accountService.CreateAsync(user, model.RegisterViewModel.Password);

                if (result.Result is ResultOfCreation.Success)
                {
                    await _emailService.SendEmailAsync(
                        model.RegisterViewModel.Email, 
                        $"Добро пожаловать, {model.RegisterViewModel.UserName}!", 
                        "ЙО!");
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error);
            }

            return View("Register", model);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View("Register", new LayoutViewModel
            {
                RegisterViewModel = new RegisterViewModel(),
                LoginViewModel = new LoginViewModel {ReturnUrl = returnUrl}
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LayoutViewModel model)
        {
            if (!ModelState.IsValid) return View("Login", model);
            User user = await _userManager.FindByEmailAsync(model.LoginViewModel.Email)
                        ?? await _userManager.FindByNameAsync(model.LoginViewModel.Email);
            if (user == null)
            {
                ModelState.AddModelError("Email", "Неправильный логин");
                return View("Login", model);
            }

            SignInResult result = await _signInManager.PasswordSignInAsync(
                user,
                model.LoginViewModel.Password,
                model.LoginViewModel.RememberMe,
                false
            );

            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(model.LoginViewModel.ReturnUrl) &&
                    Url.IsLocalUrl(model.LoginViewModel.ReturnUrl))
                    return Redirect(model.LoginViewModel.ReturnUrl);
                await _emailService.SendEmailAsync("pasko.vitaliy24@gmail.com", "Тема письма", "ЙО!");
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("Password", "Неправильный пароль");

            return View("Login", model);
        }

        [HttpGet]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public List<User> SearchUsers(string criterion, string filter)
        {
            List<User> users = _db.Users.ToList();
            switch (criterion)
            {
                case "login":
                    users = users.Where(u => u.UserName.Contains(filter)).ToList();
                    break;
                case "email":
                    users = users.Where(u => u.Email.Contains(filter)).ToList();
                    break;
            }

            return users;
        }
    }
}