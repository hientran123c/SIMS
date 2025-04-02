using UserManagement.Controllers;

namespace UserManagement.Factories
{
    public interface ICourseControllerFactory
    {
        CourseController Create();
    }
}
