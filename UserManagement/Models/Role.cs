﻿using System.ComponentModel.DataAnnotations;

namespace UserManagement.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }
    }
}