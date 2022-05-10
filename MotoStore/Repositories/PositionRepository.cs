using System.Collections.Generic;
using System.Linq;
using MotoStore.Models;
using MotoStore.Repositories.Abstractions;

namespace MotoStore.Repositories
{
    public class PositionRepository : IPositionRepository
    {
        private readonly StoreApplicationContext _db;

        public PositionRepository(StoreApplicationContext db)
        {
            _db = db;
        }

        public IEnumerable<Position> GetAll()
        {
            return _db.Positions;
        }

        public Position GetById(string id)
        {
            return _db.Positions.FirstOrDefault(p => p.Id == id);
        }

        public void Create(Position item)
        {
            _db.Positions.Add(item);
            _db.SaveChanges();
        }

        public void Update(Position item)
        {
            _db.Positions.Update(item);
            _db.SaveChanges();
        }

        public void DeleteById(string id)
        {
            var position = _db.Positions.FirstOrDefault(p => p.Id == id);
            if (position == null) return;
            position.Deleted = true;
            _db.Positions.Update(position);
            _db.SaveChanges();
        }
    }
}