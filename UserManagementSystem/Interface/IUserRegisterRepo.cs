using Microsoft.EntityFrameworkCore;
using UserManagementSystem.Models;

namespace UserManagementSystem.Interface
{
    public interface IUserRegisterRepo
    {
        Task<UserRegister?> Signup(UserRegister userRegister);
        Task<string?> Signin(UserRegister userRegister);
    }
}
