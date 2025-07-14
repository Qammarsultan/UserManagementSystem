using UserManagementSystem.Models;

namespace UserManagementSystem.Interface
{
    public interface IUserInfo
    {
        Task<UserInfo?> CreateUser(UserInfo user);
        Task<int> DeleteUser(int Id);
        Task<int> EditUser(UserInfo user);
        Task<UserInfo?> GetUserById(int id);
        Task<IEnumerable<UserInfo>> GetUsersList();
    }
}
