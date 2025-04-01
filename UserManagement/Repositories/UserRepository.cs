using Microsoft.EntityFrameworkCore;
using UserManagement.Data;
using UserManagement.Models;

namespace UserManagement.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool CreateUser(User user)
        {
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating user: {ex.Message}");
                return false;
            }
        }

        public bool UpdateUser(User user)
        {
            try
            {
                _context.Users.Update(user);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteUser(int id)
        {
            try
            {
                var user = _context.Users.Find(id);
                if (user != null)
                {
                    _context.Users.Remove(user);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IEnumerable<User> GetAllUsers()
        {
            try
            {
                return _context.Users.ToList();
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<User>();
            }
        }

        public User GetUserById(int id)
        {
            try
            {
                return _context.Users.Find(id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public User GetUserByUsername(string username)
        {
            try
            {
                return _context.Users.FirstOrDefault(u => u.Username == username);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public User GetUserByUsernameAndPassword(string username, string password)
        {
            try
            {
                return _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IEnumerable<Role> GetRoles()
        {
            return _context.Roles?.ToList() ?? new List<Role>();
        }

        public bool AssignCourseToUser(int userId, int courseId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId && u.RoleId == 2);
            if (user == null)
            {
                return false; 
            }

            var userCourse = new UserCourse
            {
                UserId = userId,
                CourseId = courseId
            };
            _context.UserCourses.Add(userCourse);
            _context.SaveChanges();
            return true;
        }

    }
}

