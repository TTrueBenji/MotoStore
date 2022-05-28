using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MotoStore.Services.Abstractions;
using MotoStore.ViewModels.Layout;
using MotoStore.ViewModels.Positions;

namespace MotoStore.Controllers
{
    public class SearchPositionController  : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IPositionService _positionService;

        public SearchPositionController(
            ILogger<AccountController> logger, 
            IPositionService positionService)
        {
            _logger = logger;
            _positionService = positionService;
        }

        [HttpGet]
        public async Task<IActionResult> Search(string criterion)
        {
            _logger.LogInformation("{@Controller}.Search запрос: {@Criterion}", 
                typeof(SearchPositionController), criterion);
            var positions = await _positionService.GetPositionsByAnyCriterionAsync(criterion);
            LayoutViewModel layoutViewModel = new LayoutViewModel
            {
                AllPositionsViewModel = new AllPositionsViewModel
                {
                    Positions = positions
                }
            };
            TempData["model"] = JsonSerializer.Serialize(layoutViewModel);
            
            return RedirectToAction("All", "Position");
        }
    }
}