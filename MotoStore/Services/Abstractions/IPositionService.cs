using System.Collections.Generic;
using MotoStore.Models;

namespace MotoStore.Services.Abstractions
{
    public interface IPositionService
    {
        IEnumerable<Position> GetPositions();
        Position GetPositionById(string id);
        void CreatePosition(Position item);
        void UpdatePosition(Position item);
        void DeletePositionById(string id);
    }
}