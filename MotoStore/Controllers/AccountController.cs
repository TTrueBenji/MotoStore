using System.Collections.Generic;
using System.IO;
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
        private readonly IFileUploadService _fileUploadService;

        public AccountController(
            StoreApplicationContext db,
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            ILogger<AccountController> logger,
            IAccountService accountService, 
            IEmailService emailService, 
            IDefaultAvatarService defaultAvatarService, 
            IWebHostEnvironment environment, 
            IFileUploadService fileUploadService)
        {
            _db = db;
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _accountService = accountService;
            _emailService = emailService;
            _defaultAvatarService = defaultAvatarService;
            _environment = environment;
            _fileUploadService = fileUploadService;
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(LayoutViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = model.RegisterViewModel.MapToUser();
                
                if (model.RegisterViewModel.File is null)
                    user.PathToAvatar = _defaultAvatarService.GetPathToDefaultAvatar(_environment);
                else
                {
                    var fileName = FileNameModifier(user.UserName, model.RegisterViewModel.File.FileName);
                    int pos = fileName.LastIndexOf('.');
                    string fileDirectoryName = fileName.Substring(0, pos);
                    string path = Path.Combine(_environment.ContentRootPath, $"wwwroot\\UserFiles\\Avatars\\{fileDirectoryName}");
                    await _fileUploadService.Upload(path, fileName, model.RegisterViewModel.File);
                    user.PathToAvatar = $"UserFiles/Avatars/{fileDirectoryName}/{fileName}";
                }
                
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
                    ModelState.AddModelError(string.Empty, $"Логин {user.UserName} занят.");
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
                // await _emailService.SendEmailAsync("pasko.vitaliy24@gmail.com", "Тема письма", "ЙО!");
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
        
        private string FileNameModifier(string login, string fileName)
        {
            return login + "." + fileName.Split('.')[1];
        }
    }
}