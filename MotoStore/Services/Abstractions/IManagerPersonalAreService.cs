using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using MotoStore.Models;

namespace MotoStore.Services.Abstractions
{
    public interface IManagerPersonalAreService
    {
        IEnumerable<User> GetAllUsers();
        Task<IEnumerable<User>> GetAllUsersInRoleAsync();
    }
}