using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MotoStore.Enums;
using MotoStore.Models;

namespace MotoStore.Services
{
    public class RolesInitializeService
    {
        public static async Task SeedAdminUser(
            RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager)
        {
            PasswordHasher<User> hasher = new ();
            string managerEmail = "manager@manager.com";
            string managerPassword = "manager";
            
            string adminEmail = "admin@admin.com";
            string adminPassword = "admin";

            var roles = new [] { Roles.Manager.ToString(), Roles.User.ToString(), Roles.Admin.ToString() };

            foreach (var role in roles)
            {
                if (await roleManager.FindByNameAsync(role) is null)
                    await roleManager.CreateAsync(new IdentityRole(role));
            }
            if (await userManager.FindByNameAsync(managerEmail) == null)
            {
                User manager = new User { Email = managerEmail, UserName = managerEmail};
                manager.PasswordHash = hasher.HashPassword(manager, managerPassword);
                IdentityResult result = await userManager.CreateAsync(manager, managerPassword);
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(manager, Roles.Manager.ToString());
            }
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                User admin = new User { Email = adminEmail, UserName = adminEmail};
                admin.PasswordHash = hasher.HashPassword(admin, adminPassword);
                IdentityResult result = await userManager.CreateAsync(admin, adminPassword);
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(admin, Roles.Admin.ToString());
            }
        }
    }
}