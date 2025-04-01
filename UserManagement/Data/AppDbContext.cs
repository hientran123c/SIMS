using Microsoft.EntityFrameworkCore;
using UserManagement.Models;

namespace UserManagement.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }   
        public DbSet<UserManagement.Models.User> Users { get; set; }
        public DbSet<UserManagement.Models.Role> Roles { get; set; }
        public DbSet<UserManagement.Models.Course> Courses { get; set; }
        public DbSet<UserManagement.Models.UserCourse> UserCourses { get; set; }
    }
}
