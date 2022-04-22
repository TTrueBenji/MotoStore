using System.Collections.Generic;
using MotoStore.Exceptions;
using MotoStore.Models;
using MotoStore.Repositories.Abstractions;
using MotoStore.Services.Abstractions;

namespace MotoStore.Services
{
    public class PositionService : IPositionService
    {
        private readonly IPositionRepository _positionRepository;

        public PositionService(IPositionRepository positionRepository)
        {
            _positionRepository = positionRepository;
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

        public void CreatePosition(Position position)
        {
            if (position is null)
                throw new EntityNotFoundException(nameof(Position));
            
            _positionRepository.Create(position);
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
    }
}