using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MotoStore.MapConfigurations;
using MotoStore.Models;
using MotoStore.Services.Abstractions;
using MotoStore.ViewModels.Account;

namespace MotoStore.Controllers
{
    [Authorize(Roles = "User")]
    public class UserPersonalAreaController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<User> _userManager;

        public UserPersonalAreaController(
            IOrderService orderService, 
            UserManager<User> userManager)
        {
            _orderService = orderService;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index(string userId)
        {
            IEnumerable<Order> orders = _orderService.GetOrdersByUserId(userId);
            LayoutViewModel layoutViewModel = new LayoutViewModel
            {
                OrderViewModels = orders.MapToOrderViewModels().ToList()
            };
            return View(layoutViewModel);
        }
    }
}