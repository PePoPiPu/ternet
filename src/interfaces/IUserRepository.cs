using ternet.entities;

namespace ternet.interfaces
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        User GetUserInfoByName(string userName);
        bool LogInUser(string userName, string password);
        int CheckAdminPrivileges (string userName);
        void InsertUser(string userName, string userPass, bool isAdmin);
        void UpdateUser(int userId, string userName, string userPass, bool isAdmin);
        void DeleteUser(int userId);
    }
}