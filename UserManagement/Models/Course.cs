using System.ComponentModel.DataAnnotations;

namespace UserManagement.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        [Required]
        public int Credit { get; set; } 

        public DateTime CreatedAt { get; set; }
    }
}

