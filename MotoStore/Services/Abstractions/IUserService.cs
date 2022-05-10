using System.Collections.Generic;
using System.Threading.Tasks;
using MotoStore.Models;

namespace MotoStore.Services.Abstractions
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        Task<IEnumerable<User>> GetAllUsersInRoleAsync(string role);
    }
}