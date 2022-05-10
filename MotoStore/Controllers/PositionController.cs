using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MotoStore.MapConfigurations;
using MotoStore.Services.Abstractions;
using MotoStore.ViewModels.Account;
using MotoStore.ViewModels.Layout;
using MotoStore.ViewModels.Positions;

namespace MotoStore.Controllers
{
    
    public class PositionController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IHostEnvironment _environment;
        private readonly IPositionService _positionService;
        

        public PositionController(
            IHostEnvironment environment,
            ILogger<AccountController> logger, 
            IPositionService positionService)
        {
            _environment = environment;
            _logger = logger;
            _positionService = positionService;
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Add(LayoutViewModel model)
        {
            //TODO: Добавить Try Catch и страницу ошибок.
            if (!ModelState.IsValid) return View(model);
            await _positionService.CreatePosition(model.CreatePositionViewModel, _environment);
            var positions = _positionService.GetPositions().ToList();
            return RedirectToAction("All", positions);

        }

        [HttpGet]
        public IActionResult All(LayoutViewModel model = null)
        {
            if (model!.AllPositionsViewModel is not null)
                return View(model);

            if (TempData["model"] is not null)
            {
                var test = JsonSerializer.Deserialize<LayoutViewModel>((string)TempData["model"]);
                return View(test);
            }
            
            //TODO: Сделать паггинацию 
            var positions = _positionService.GetPositions().ToList();
            var layoutViewModel = new LayoutViewModel
            {
                AllPositionsViewModel = new AllPositionsViewModel 
                    {Positions = positions.MapToShortInfoList()}
            };
            return View(layoutViewModel);
        }

        [HttpGet]
        public IActionResult Position(string id)
        {
            //TODO: Сделать паггинацию 
            var positions = _positionService.GetPositions().ToList();
            var positionViewModel = _positionService
                .GetPositionById(id)
                .MapToPositionInfoViewModel();
            
            var layoutModel = new LayoutViewModel
            {
                AllPositionsViewModel = new AllPositionsViewModel
                {
                    Positions = positions.MapToShortInfoList(),
                    PositionInfo = positionViewModel
                },
                LoginViewModel = new LoginViewModel(),
                RegisterViewModel = new RegisterViewModel(),
                CreatePositionViewModel = new CreatePositionViewModel()
            };
            TempData["model"] = JsonSerializer.Serialize(layoutModel);
            return RedirectToAction("All", "Position");
        }
    }
}