using Microsoft.AspNetCore.Identity;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Infrastructure.Seeder;

public static class UserSeeder
{
    public static async Task SeedAsync(UserManager<User> userManager)
    {
        var usersCount = await userManager.Users.CountAsync();
        if (usersCount <= 0)
        {
            var defaultUser = new User()
            {
                UserName = "School@Project.com",
                Email = "School@Project.com",
                FullName = "SchoolProject.com",
                Country = "Ukraine",
                PhoneNumber = "123-456-789",
                Address = "Kharkiv",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            await userManager.CreateAsync(defaultUser, "Allcombo8@");
            await userManager.AddToRolesAsync(defaultUser, new[] { "admin" });
        }
    }
}