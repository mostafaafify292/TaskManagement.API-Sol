using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TaskManagement.Repository.Data
{
    public static class RoleIdentityDbContext
    {
        public static async Task SeedRoleAsync(RoleManager<IdentityRole> roleManager)
        {

            // Define roles
            string[] roles = { "Admin", "User" };

            foreach (var role in roles)
            {
                // Check if the role exists, and create it if not
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
