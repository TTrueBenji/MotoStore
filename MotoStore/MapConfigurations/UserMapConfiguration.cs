using System.Collections.Generic;
using System.Linq;
using MotoStore.Models;
using MotoStore.ViewModels.UsersPersonalArea;

namespace MotoStore.MapConfigurations
{
    public static class UserMapConfiguration
    {
        public static List<UsersPersonalAreaViewModel> MapToUsersPersonalAreaViewModels(this IEnumerable<User> users)
        {
            return users.Select(u => new UsersPersonalAreaViewModel
            {
                Id = u.Id,
                Address = u.Address,
                City = u.City,
                Email = u.Email,
                Username = u.UserName,
                PhoneNumber = u.PhoneNumber,
                CreationDateTime = u.CreationDateTime
            }).ToList();
        }
    }
}