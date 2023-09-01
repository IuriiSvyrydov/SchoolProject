using Microsoft.AspNetCore.Identity;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Infrastructure.Seeder;

public static class RoleSeeder
{
    public static async Task SeedAsync(RoleManager<Role> roleManager)
    {
        var rolesCount = await roleManager.Roles.CountAsync();

        if (rolesCount <= 0)
        {
            await roleManager.CreateAsync(new Role()
            {
                Name = "User"
            });
        }
    }
}