using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using MotoStore.Models;
using MotoStore.ViewModels.Positions;

namespace MotoStore.Services.Abstractions
{
    public interface IPositionService
    {
        IEnumerable<Position> GetPositions();
        Position GetPositionById(string id);
        Task CreatePosition(CreatePositionViewModel positionViewModel, IHostEnvironment environment);
        Task<List<PositionShortInfoViewModel>> GetPositionsByAnyCriterionAsync(string criterion);
        void UpdatePosition(Position item);
        void DeletePositionById(string id);
    }
}