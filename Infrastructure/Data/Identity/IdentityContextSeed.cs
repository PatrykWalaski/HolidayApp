using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data.Identity
{
    public class IdentityContextSeed
    {
        public static async Task SeedAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if(!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if(!userManager.Users.Any())
            {
                var adminUser = new IdentityUser
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                };

                var user = new IdentityUser
                {
                    UserName = "Bob",
                    Email = "bob@gmail.com"
                };

                await userManager.CreateAsync(adminUser, "Pa$$w0rd");
                await userManager.CreateAsync(user, "Pa$$w0rd");

                var createdAdmin = await userManager.FindByEmailAsync(adminUser.Email);
                await userManager.AddToRoleAsync(createdAdmin, "Admin");

            }


        }
    }
}