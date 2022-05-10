using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MotoStore.Models;
using MotoStore.Repositories.Abstractions;

namespace MotoStore.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;

        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IEnumerable<User>> GetAllInRoleAsync(string role)
        {
            //TODO: Паггинация
            return  await _userManager.GetUsersInRoleAsync(role);
        }

        public IEnumerable<User> GetAll()
        {
            return _userManager.Users;
        }

        public User GetById(string id)
        {
            throw new System.NotImplementedException();
        }

        public void Create(User item)
        {
            throw new System.NotImplementedException();
        }

        public void Update(User item)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteById(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}