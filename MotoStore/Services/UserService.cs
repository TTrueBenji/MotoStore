using System.Collections.Generic;
using System.Threading.Tasks;
using MotoStore.Models;
using MotoStore.Repositories.Abstractions;
using MotoStore.Services.Abstractions;

namespace MotoStore.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetAll();
        }

        public Task<IEnumerable<User>> GetAllUsersInRoleAsync(string role)
        {
            return _userRepository.GetAllInRoleAsync(role);
        }
    }
}