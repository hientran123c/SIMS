using UserManagement.Models;

namespace UserManagement.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();

        User GetUserById(int id);

        User GetUserByUsernameAndPassword(string username, string password);

        User GetUserByUsername(string username);

        bool CreateUser(User user);

        bool UpdateUser(User user);

        bool DeleteUser(int id);
        IEnumerable<Role> GetRoles();

    }
}
