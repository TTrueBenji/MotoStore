using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotoStore.MapConfigurations;
using MotoStore.Models;
using MotoStore.Services.Abstractions;
using MotoStore.ViewModels.Layout;

namespace MotoStore.Controllers
{
    [Authorize(Roles = "User")]
    public class UserPersonalAreaController : Controller
    {
        private readonly IOrderService _orderService;

        public UserPersonalAreaController(
            IOrderService orderService)
        {
            _orderService = orderService;
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