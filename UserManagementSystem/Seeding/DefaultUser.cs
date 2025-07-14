using Microsoft.AspNetCore.Identity;
using UserManagementSystem.Identity;

namespace UserManagementSystem.Seeding
{
    public static class DefaultUser 
    {
        public async static Task SeedUserAsync(IServiceProvider serviceProvider)
        {
           var userManager =  serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var user = new ApplicationUser();
            user.UserName = "Admin";
            user.Email = "Hanif@gmail.com";

            if (userManager.Users.All(u => u.Id == user.Id))
            {
                var result =await userManager.FindByEmailAsync(user.Email);  
                if(result == null){
                  await userManager.CreateAsync(user , "123@Hanif");
                   await userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }
    }
}
