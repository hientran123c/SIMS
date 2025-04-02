using UserManagement.Controllers;
using UserManagement.Repositories;

namespace UserManagement.Factories
{
    public class CourseControllerFactory : ICourseControllerFactory
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IUserRepository _userRepository;

        public CourseControllerFactory(ICourseRepository courseRepository, IUserRepository userRepository)
        {
            _courseRepository = courseRepository;
            _userRepository = userRepository;
        }

        public CourseController Create()
        {
            return new CourseController(_courseRepository, _userRepository);
        }
    }
}
