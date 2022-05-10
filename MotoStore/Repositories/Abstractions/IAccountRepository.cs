using System.Collections.Generic;
using System.Threading.Tasks;
using CommonData;
using Microsoft.AspNetCore.Identity;
using MotoStore.Models;

namespace MotoStore.Repositories.Abstractions
{
    public interface IAccountRepository
    {
        IEnumerable<User> GetAll();
        User GetById(string id);
        Task<IdentityResult> CreateAsync(User user, string password);
        void Update(User user);
        void DeleteById(string id);
        Task<ResultOfCreation> AddToRole(User user);
    }
}