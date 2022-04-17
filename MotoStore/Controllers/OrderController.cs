using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MotoStore.MapConfigurations;
using MotoStore.Models;
using MotoStore.ViewModels.Account;

namespace MotoStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly StoreApplicationContext _db;

        public OrderController(
            ILogger<AccountController> logger, 
            UserManager<User> userManager, 
            StoreApplicationContext db)
        {
            _logger = logger;
            _userManager = userManager;
            _db = db;
        }

        // [Authorize("user")]
        [HttpPost]
        public IActionResult Create(LayoutViewModel model)
        {
            Order order = model.OrderCreateViewModel.MapToOrder();
            order.UserId = _userManager.GetUserId(User);
            _db.Orders.Add(order);
            _db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}