using UserManagementSystem.Data;
using UserManagementSystem.Models;

namespace UserManagementSystem.Seeding
{
    public static class SeedAdminUser
    {
        public static async Task SeedAdminUserAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // Ensure DB is created
            await _context.Database.EnsureCreatedAsync();

            // Check if admin exists
            if (!_context.Registers.Any(u => u.Email == "admin@example.com"))
            {
                var adminUser = new UserRegister
                {
                    UserName = "Qammar",
                    Email = "admin@gmail.com",
                    PasswordHash = "123", 
                    Role = "Admin",
                };

                _context.Registers.Add(adminUser);
                await _context.SaveChangesAsync();
            }
        }
    }
}
