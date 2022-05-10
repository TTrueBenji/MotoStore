using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotoStore.Services.Abstractions;
using MotoStore.ViewModels.Layout;

namespace MotoStore.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ManagerPersonalAreaController : Controller
    {
        private readonly IManagerPersonalAreService _managerPersonalAreaService;

        public ManagerPersonalAreaController(IManagerPersonalAreService managerPersonalAreaService)
        {
            _managerPersonalAreaService = managerPersonalAreaService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _managerPersonalAreaService.GetAllUsersInRoleAsync();
            LayoutViewModel layoutViewModel = new LayoutViewModel
            {
                Users = users.ToList()
            };
            return View(layoutViewModel);
        }
    }
}