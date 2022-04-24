using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MotoStore.MapConfigurations;
using MotoStore.Models;
using MotoStore.Services.Abstractions;
using MotoStore.ViewModels.Account;
using MotoStore.ViewModels.Layout;

namespace MotoStore.Controllers
{
    [Authorize(Roles = "User")]
    public class OrderController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly IOrderService _orderService;

        public OrderController(
            ILogger<AccountController> logger, 
            UserManager<User> userManager, 
            IOrderService orderService)
        {
            _logger = logger;
            _userManager = userManager;
            _orderService = orderService;
        }
        
        [HttpPost]
        public IActionResult Create(LayoutViewModel model)
        {
            Order order = model.OrderCreateViewModel.MapToOrder();
            order.UserId = _userManager.GetUserId(User);
            _logger.LogInformation("{@Controller}.Create данные: {@Data}, USerId: {@UserId}", 
                GetType(), model.OrderCreateViewModel, order.UserId);
            _orderService.CreateOrder(order);
            
            return RedirectToAction("Index", "Home");
        }
    }
}