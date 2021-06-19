using Microsoft.AspNetCore.Identity;
using ProgrammerBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammerBlog.Data.Concrete.EntityFramework.Context
{
    public static class SeedIdentity
    {
        public static async Task Seed(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
            var adminUser = new User()
            {
                UserName = "adminuser",
                Email="acanakdas@gmail.com",
                FirstName="Ahmet Can",
                LastName="Akdaş",
                EmailConfirmed=true,
                PhoneNumberConfirmed=true,
                
            };
            var result = await userManager.CreateAsync(adminUser,"adminuser");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "adminuser");
            }
            
        }
    }
}
