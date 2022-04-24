using System.Collections.Generic;
using System.Threading.Tasks;
using MotoStore.Models;

namespace MotoStore.Repositories.Abstractions
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetAllInRoleAsync(string role);
    }
}