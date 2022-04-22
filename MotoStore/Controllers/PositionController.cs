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
using MotoStore.ViewModels.Positions;

namespace MotoStore.Controllers
{
    
    public class PositionController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IHostEnvironment _environment;
        private readonly IFileUploadService _fileUploadService;
        private readonly IPositionService _positionService;
        

        public PositionController(
            IHostEnvironment environment,
            IFileUploadService fileUploadService, 
            ILogger<AccountController> logger, 
            IPositionService positionService)
        {
            _environment = environment;
            _fileUploadService = fileUploadService;
            _logger = logger;
            _positionService = positionService;
        }

        [HttpGet]
        [Authorize(Roles = "manager")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "manager")]
        public async Task<IActionResult> Add(LayoutViewModel model)
        {
            //TODO: Добавить Try Catch и страницу ошибок.
            if (ModelState.IsValid)
            {
                string directoryName = DirectoryNameModifier(model.CreatePositionViewModel.Model);
                string path = Path.Combine(_environment.ContentRootPath,
                    $"wwwroot\\Images\\Positions\\{directoryName}\\");
                string fileName = FileNameModifier(model.CreatePositionViewModel.Model,
                    model.CreatePositionViewModel.Image.FileName);
                await _fileUploadService.Upload(path,  fileName, model.CreatePositionViewModel.Image);
                var position = model.CreatePositionViewModel.MapToPosition();
                string imagePath = $"Images/Positions/{directoryName}/{fileName}";
                position.PathToImage = imagePath;
                _positionService.CreatePosition(position);
                
                var positions = _positionService.GetPositions().ToList();
                return RedirectToAction("All", positions);
            }

            return View(model);
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

        [NonAction]
        private string FileNameModifier(string model, string fileName)
        {
            string modelName = DirectoryNameModifier(model);
            return modelName + "." + fileName.Split('.')[1];
        }
        
        [NonAction]
        private string DirectoryNameModifier(string modelName)
        {
            var modelNameParts = modelName.Split();
            string modifiedName = string.Empty;

            for (int i = 0; i < modelNameParts.Length; i++)
            {
                if (i == 0)
                    modifiedName += modelNameParts[i];
                else
                    modifiedName += "_" + modelNameParts[i];
            }

            return modifiedName;
        }
    }
}