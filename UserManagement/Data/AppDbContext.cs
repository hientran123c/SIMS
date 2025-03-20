using Microsoft.EntityFrameworkCore;

namespace UserManagement.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }   
        public DbSet<UserManagement.Models.User> Users { get; set; }
        public DbSet<UserManagement.Models.Role> Roles { get; set; }   
    }
}
