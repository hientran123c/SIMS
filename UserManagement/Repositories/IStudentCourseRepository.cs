using UserManagement.Models;

namespace UserManagement.Repositories
{
    public interface IStudentCourseRepository
    {
        void AddStudentCourse(StudentCourse studentCourse);
        IEnumerable<StudentCourse> GetStudentCoursesByStudentId(int studentId);
    }
}
