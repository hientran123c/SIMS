using UserManagement.Models;
using UserManagement.Repositories;

namespace UserManagement.Services
{
    public class StudentCourseService
    {
        private readonly IStudentCourseRepository _repository;

        public StudentCourseService(IStudentCourseRepository repository)
        {
            _repository = repository;
        }

        public void AddCourseForStudent(int studentId, int courseId, DateTime beginTime, DateTime endTime)
        {
            var studentCourse = new StudentCourse
            {
                StudentId = studentId,
                CourseId = courseId,
                BeginTime = beginTime,
                EndTime = endTime
            };

            _repository.AddStudentCourse(studentCourse);
        }

        public IEnumerable<StudentCourse> GetCoursesForStudent(int studentId)
        {
            return _repository.GetStudentCoursesByStudentId(studentId);
        }
    }
}
