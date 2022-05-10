using System.Collections.Generic;
using System.Threading.Tasks;
using CommonData;
using Microsoft.AspNetCore.Identity;
using MotoStore.DataObjects.Account;
using MotoStore.Models;
using MotoStore.Repositories.Abstractions;
using MotoStore.Services.Abstractions;

namespace MotoStore.Services
{
    public class AccountService : IAccountService
    {

        private readonly IAccountRepository _accountRepository;
        private readonly SignInManager<User> _signInManager;

        public AccountService(
            IAccountRepository accountRepository, 
            SignInManager<User> signInManager)
        {
            _accountRepository = accountRepository;
            _signInManager = signInManager;
        }

        public async Task<CreationResultDataObject> CreateAsync(User user, string password)
        {
            CreationResultDataObject creationResult = new CreationResultDataObject();
            var result = await _accountRepository.CreateAsync(user, password);
            if (result.Succeeded)
            {
               var resultOfAddingToRole = await _accountRepository.AddToRole(user);
               
               if (resultOfAddingToRole is not ResultOfCreation.Success) return creationResult;
               
               await _signInManager.SignInAsync(user, false);
               creationResult = new CreationResultDataObject
               {
                   Errors = new List<string>(),
                   Result = ResultOfCreation.Success
               };
               
               return creationResult;
            }

            List<string> errors = new ();
            foreach (var error in result.Errors)
                errors.Add(error.Description);
            creationResult = new CreationResultDataObject
            {
                Errors = errors,
                Result = ResultOfCreation.Failed
            };
            
            return creationResult;
        }

        public void Update(User user)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteById(string id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public User GetById(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}