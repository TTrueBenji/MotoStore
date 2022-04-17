using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MotoStore.Models;

namespace MotoStore.Services
{
    public class RolesInitializeService
    {
        public static async Task SeedAdminUser(
            RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager)
        {
            string adminEmail = "manager@manager.com";
            string password = "password";
  
            var roles = new [] { "manager", "user" };

            foreach (var role in roles)
            {
                if (await roleManager.FindByNameAsync(role) is null)
                    await roleManager.CreateAsync(new IdentityRole(role));
            }
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                User admin = new User { Email = adminEmail, UserName = adminEmail, Password = password};
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(admin, "manager");
            }
        }
    }
}