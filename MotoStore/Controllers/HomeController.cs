using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MotoStore.ViewModels;
using MotoStore.ViewModels.Account;
using MotoStore.ViewModels.Layout;

namespace MotoStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            LayoutViewModel layoutViewModel = new LayoutViewModel
            {
                RegisterViewModel = new RegisterViewModel(),
                LoginViewModel = new LoginViewModel()
            };
            return View(layoutViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}