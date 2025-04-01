using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.Models
{
    public class StudentCourse
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        [Required]
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        [Required]
        [int Range(1, 100)]
        public int Course { get; set; }
    }
}
