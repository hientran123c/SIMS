using System.ComponentModel.DataAnnotations;

namespace UserManagement.Models
{
    public class UserCourse
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public ICollection<User> Users { get; set; }
        public int CourseId { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
