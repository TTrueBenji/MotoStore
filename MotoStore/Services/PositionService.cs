using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MotoStore.Exceptions;
using MotoStore.MapConfigurations;
using MotoStore.Models;
using MotoStore.Repositories.Abstractions;
using MotoStore.Services.Abstractions;
using MotoStore.ViewModels.Positions;

namespace MotoStore.Services
{
    public class PositionService : IPositionService
    {
        private readonly IPositionRepository _positionRepository;
        private readonly IFileUploadService _fileUploadService;

        public PositionService(
            IPositionRepository positionRepository, 
            IFileUploadService fileUploadService)
        {
            _positionRepository = positionRepository;
            _fileUploadService = fileUploadService;
        }

        public IEnumerable<Position> GetPositions()
        {
            return _positionRepository.GetAll();
        }

        public Position GetPositionById(string id)
        {
            var position = _positionRepository.GetById(id);
            if (position is null)
                throw new EntityNotFoundException(nameof(Position), id);

            return position;
        }

        public async Task CreatePosition(CreatePositionViewModel positionViewModel, IHostEnvironment environment)
        {
            if (positionViewModel is null)
                throw new EntityNotFoundException(nameof(Position));
            var test = environment.ContentRootPath;
            string directoryName = DirectoryNameModifier(positionViewModel.Manufacturer);
            string path = Path.Combine(environment.ContentRootPath,
                $"wwwroot\\Images\\Positions\\{directoryName}\\");
            string fileName = FileNameModifier(positionViewModel.Model, positionViewModel.Image.FileName);
            await _fileUploadService.Upload(path,  fileName, positionViewModel.Image);
            var position = positionViewModel.MapToPosition();
            position.PathToImage = $"Images/Positions/{directoryName}/{fileName}";
            _positionRepository.Create(position);
        }

        public async Task<List<PositionShortInfoViewModel>> GetPositionsByAnyCriterionAsync(string criterion)
        {
            if (criterion.IsNullOrEmpty())
            {
                return _positionRepository.GetAll()
                    .ToList()
                    .MapToShortInfoList();
            }
            criterion = criterion.ToUpper();
            var allPositions = _positionRepository.GetAllAsQueryable();
            var positionsAsQueryable = allPositions.Where(p => 
                p.Model.ToUpper().Contains(criterion) || 
                p.Manufacturer.ToUpper().Contains(criterion));
            var positionsTask = positionsAsQueryable.ToListAsync();
            var positions = await positionsTask;
            
            return positions.MapToShortInfoList();
        }

        public void UpdatePosition(Position position)
        {
            if (position is null)
                throw new EntityNotFoundException(nameof(Position));
            _positionRepository.Update(position);
        }

        public void DeletePositionById(string id)
        {
            _positionRepository.DeleteById(id);
        }

        private string FileNameModifier(string model, string fileName)
        {
            string modelName = DirectoryNameModifier(model);
            return modelName + "." + fileName.Split('.')[1];
        }
        
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