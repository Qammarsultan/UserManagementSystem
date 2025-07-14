using Microsoft.AspNetCore.Identity;
using UserManagementSystem.Identity;

namespace UserManagementSystem.Seeding
{
    public static class DefaultRoles
    {
        public async static Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
           var roleManager =  serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            var admin = new ApplicationRole();
            admin.Name = "Admin".ToUpper();
            admin.NormalizedName = "Admin".ToUpper();
            await roleManager.CreateAsync(admin);

            var user = new ApplicationRole();
            user.Name = "User".ToUpper();
            user.NormalizedName = "User".ToUpper();
            await roleManager.CreateAsync(user);
        }

    }
}
