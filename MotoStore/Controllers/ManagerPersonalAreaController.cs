using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotoStore.MapConfigurations;
using MotoStore.Services.Abstractions;
using MotoStore.ViewModels.Layout;
using MotoStore.ViewModels.UsersPersonalArea;
using static MoreLinq.Extensions.LagExtension;
using static MoreLinq.Extensions.LeadExtension;

namespace MotoStore.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ManagerPersonalAreaController : Controller
    {
        private readonly IManagerPersonalAreService _managerPersonalAreaService;
        private readonly IOrderService _orderService;

        public ManagerPersonalAreaController(IManagerPersonalAreService managerPersonalAreaService, IOrderService orderService)
        {
            _managerPersonalAreaService = managerPersonalAreaService;
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _managerPersonalAreaService.GetAllUsersInRoleAsync();
            
            LayoutViewModel layoutViewModel = new LayoutViewModel
            {
                ManagerPersonalAreaUsers = users.MapToUsersPersonalAreaViewModels()
            };
            layoutViewModel.ManagerPersonalAreaUsers.ForEach(u =>
            {
                u.OrdersCount = _orderService.GetOrdersByUserId(u.Id).Count();
            });
            
            return View(layoutViewModel);
        }

        [HttpGet]
        public IActionResult UserOrders(string userId)
        {
            var orders = _orderService.GetOrdersByUserId(userId);
            LayoutViewModel layoutViewModel = new LayoutViewModel
            {
                OrderViewModels = orders.MapToOrderViewModels()
            };
            return View(layoutViewModel);
        }
    }
}