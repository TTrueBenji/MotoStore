using System.Collections.Generic;
using System.Threading.Tasks;
using MotoStore.DataObjects.Account;
using MotoStore.Models;

namespace MotoStore.Services.Abstractions
{
    public interface IAccountService
    {
        Task<CreationResultDataObject> CreateAsync(User user, string password);
        void Update(User user);
        void DeleteById(string id);
        IEnumerable<User> GetAll();
        User GetById(string id);
    }
}