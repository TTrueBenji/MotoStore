using System.Linq;
using MotoStore.Models;

namespace MotoStore.Repositories.Abstractions
{
    public interface IPositionRepository : IRepository<Position>
    {
        IQueryable<Position> GetAllAsQueryable();
    }
}