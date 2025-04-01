using UserManagement.Data;
using UserManagement.Models;

namespace UserManagement.Repositories
{
    public class StudentCourseRepository : IStudentCourseRepository
    {
        private readonly AppDbContext _context;

        public StudentCourseRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AddStudentCourse(StudentCourse studentCourse)
        {
            _context.StudentCourses.Add(studentCourse);
            _context.SaveChanges();
        }

        public IEnumerable<StudentCourse> GetStudentCoursesByStudentId(int studentId)
        {
            return _context.StudentCourses.Where(sc => sc.StudentId == studentId).ToList();
        }
    }
}
