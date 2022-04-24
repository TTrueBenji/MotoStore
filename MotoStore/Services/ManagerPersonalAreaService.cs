using System.Collections.Generic;
using System.Threading.Tasks;
using MotoStore.Models;
using MotoStore.Services.Abstractions;

namespace MotoStore.Services
{
    public class ManagerPersonalAreaService : IManagerPersonalAreService
    {
        private readonly IUserService _userService;

        public ManagerPersonalAreaService(IUserService userService)
        {
            _userService = userService;
        }
        
        public IEnumerable<User> GetAllUsers()
        {
            return _userService.GetAllUsers();
        }

        public Task<IEnumerable<User>> GetAllUsersInRoleAsync()
        {
            return _userService.GetAllUsersInRoleAsync("User");
        }
    }
}