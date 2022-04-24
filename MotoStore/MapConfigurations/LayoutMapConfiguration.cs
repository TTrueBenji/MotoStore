using System;
using MotoStore.Models;
using MotoStore.ViewModels.Account;

namespace MotoStore.MapConfigurations
{
    public static class LayoutMapConfiguration
    {
        public static User MapToUser(this RegisterViewModel model)
        {
            return new User 
            {
                Email = model.Email,
                UserName = model.UserName,
                // PathToAvatar = model.PathToAvatar,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                CreationDateTime = DateTime.Now
            };
        }
    }
}