using Microsoft.EntityFrameworkCore;
using UserManagement.Models;

namespace UserManagement.Data
{
    public class StudentCourseDbContext : DbContext
    {
        public StudentCourseDbContext(DbContextOptions<StudentCourseDbContext> options) : base(options) { }

        public DbSet<StudentCourse> StudentCourses { get; set; }
    }
}
