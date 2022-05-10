using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MotoStore.MapConfigurations;
using MotoStore.Models;
using MotoStore.Services.Abstractions;
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

        [HttpGet]
        public IActionResult Checkout(string orderId)
        {
            Order order = _orderService.GetOrderById(orderId);
            LayoutViewModel layoutViewModel = new LayoutViewModel
            {
                LiveOrderViewModel = order.MapToOrderCheckoutViewModel()
            };
            return View(layoutViewModel);
        }

        [HttpPost]
        public IActionResult Checkout(LayoutViewModel layoutViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.LogInformation("{Controller} Данные: {@Data}",
                        typeof(OrderController), layoutViewModel);
                    _orderService.CreateLiveOrder(layoutViewModel.LiveOrderViewModel);
                    return RedirectToAction(
                        "Index",
                        "UserPersonalArea",
                        new {userId = layoutViewModel.LiveOrderViewModel.UserId});
                }

                _logger.LogWarning("{Controller} модель не прошла валидацию. Данные: {@Data}",
                    typeof(OrderController), layoutViewModel);
                return View(layoutViewModel);
            }
            catch (Exception e)
            {
                _logger.LogError("{Controller} ошибка {@Message}. Данные: {@Data}",
                    typeof(OrderController), e.Message, layoutViewModel);
                return View(layoutViewModel);
            }
        }

        [HttpGet]
        public IActionResult ConfirmDelivery(string orderId)
        {
            _orderService.ConfirmLiveOrder(orderId);
            return RedirectToAction("Index", "UserPersonalArea", new {userId = _userManager.GetUserId(User)});
        }
    }
}