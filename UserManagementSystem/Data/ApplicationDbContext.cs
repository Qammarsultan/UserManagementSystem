using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserManagementSystem.Identity;
using UserManagementSystem.Models;

namespace UserManagementSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,Guid>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
       public DbSet<UserInfo> Users { get; set; }
       public DbSet<UserRegister> Registers { get; set; }
    }
}
