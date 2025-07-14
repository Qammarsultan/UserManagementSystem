using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UserManagementSystem.Data;
using UserManagementSystem.Interface;
using UserManagementSystem.Models;

namespace UserManagementSystem.Repository
{
    public class UserRegisterRepo : IUserRegisterRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        public UserRegisterRepo(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<UserRegister?> Signup(UserRegister userRegister)
        {
            if (await _context.Registers.AnyAsync(u => u.Email == userRegister.Email))
                return null;
          userRegister.Role = "User";
          await  _context.Registers.AddAsync(userRegister);  
          await  _context.SaveChangesAsync();
            return userRegister;
        }

        public async Task<string?> Signin(UserRegister userRegister)
        {
            var res =  _context.Registers.FirstOrDefault(u => u.Email == userRegister.Email);
              if (res == null)  return null;
            var passworkCheck = await _context.Registers.AnyAsync(u => u.PasswordHash == userRegister.PasswordHash);
             if (!passworkCheck)
                return null;


            var token = CreateToken(userRegister);
            return token;

        }
           
        private string CreateToken(UserRegister userRegister)
        {
            var claim = new List<Claim>
            {
                new Claim(ClaimTypes.Email , userRegister.Email),
                new Claim(ClaimTypes.Name , userRegister.UserName),
                new Claim(ClaimTypes.Role , userRegister.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.
                GetBytes(_configuration.GetValue<string>("AppSettings:Key")!));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("AppSettings:Issuer"),
                audience: _configuration.GetValue<string>("AppSettings:Audience"),
                claims:claim,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
                );
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
