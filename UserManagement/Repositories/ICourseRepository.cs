using UserManagement.Models;
using System.Collections.Generic;

namespace UserManagement.Repositories
{
    public interface ICourseRepository
    {
        IEnumerable<Course> GetAllCourses();
        Course GetCourseById(int id);
        bool CreateCourse(Course course);
        bool UpdateCourse(Course course);
        bool DeleteCourse(int id);
    }
}
