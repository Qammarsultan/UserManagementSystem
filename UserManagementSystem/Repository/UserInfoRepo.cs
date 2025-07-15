using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UserManagementSystem.Data;
using UserManagementSystem.Interface;
using UserManagementSystem.Models;

namespace UserManagementSystem.Repository
{
    public class UserInfoRepo : IUserInfo
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserInfoRepo(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<UserInfo?> CreateUser(UserInfo user)
        {
            if (await _context.Users.AnyAsync(u => u.Email == user.Email))
                return null;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }


        public async Task<UserInfo?> GetUserById(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return null;
            }
            return user;
        }
        public async Task<int> EditUser(UserInfo user)
        {
            var result = _context.Users.FirstOrDefault(u => u.Id == user.Id);
            if (result == null)
            {
                return -1;
            }
            result.FullName = user.FullName;
            result.Email = user.Email;
            result.Password = user.Password;
            result.Role = user.Role;

            await _context.SaveChangesAsync();
            return 1;
        }



        public async Task<IEnumerable<UserInfo>> GetUsersList()
        {
            var jwt = _httpContextAccessor.HttpContext?.Request.Cookies["jwt"];
            if (string.IsNullOrEmpty(jwt))
                return Enumerable.Empty<UserInfo>();

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt);

            var role = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            var email = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(role) || string.IsNullOrEmpty(email))
                return Enumerable.Empty<UserInfo>();

            if (role == "Admin")
            {
                return await _context.Users.ToListAsync();
            }
            else
            {
                return await _context.Users
                    .Where(u => u.Email == email)
                    .ToListAsync();
            }
        }


        public async Task<int> DeleteUser(int Id)
        {
            var user = await _context.Users.Where(u => u.Id == Id).FirstOrDefaultAsync();
            if (user == null)
                return -1;
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return 1;
        }
    }
}
