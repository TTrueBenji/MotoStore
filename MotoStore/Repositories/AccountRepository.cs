using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonData;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MotoStore.Enums;
using MotoStore.Models;
using MotoStore.Repositories.Abstractions;

namespace MotoStore.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<AccountRepository> _logger;

        public AccountRepository(
            UserManager<User> userManager, 
            ILogger<AccountRepository> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }
        public IEnumerable<User> GetAll()
        {
            //TODO: паггинация
            return _userManager.Users;
        }

        public User GetById(string id)
        {
            return _userManager.Users.FirstOrDefault(u => u.Id == id);
        }

        public async Task<IdentityResult> CreateAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }
        
        
        
        public void Update(User item)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteById(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task AddToRole()
        {
            throw new System.NotImplementedException();
        }

        public async Task<ResultOfCreation> AddToRole(User user)
        {
            try
            {
                await _userManager.AddToRoleAsync(user, Roles.User.ToString());
                return ResultOfCreation.Success;
            }
            catch (Exception e)
            {
                _logger.LogTrace(e.StackTrace);
                _logger.LogError("{@Repository}.SignInAndAddToRole ошибка {@Message}. Данные {@Data}" ,
                    GetType(), e.Message, user);
                return ResultOfCreation.Failed;
            }
            
        }
    }
}