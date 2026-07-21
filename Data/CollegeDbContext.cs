using CollegeInfoPortal.Models;
using Microsoft.EntityFrameworkCore;

namespace CollegeInfoPortal.Data
{
    public class CollegeDbContext : DbContext
    {
        public CollegeDbContext(DbContextOptions<CollegeDbContext> options) : base(options)
        {
        }

        public DbSet<Department> Departments { get; set; } = null!;
        public DbSet<Course> Courses { get; set; } = null!;
        public DbSet<Faculty> Faculty { get; set; } = null!;
        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<Event> Events { get; set; } = null!;
    }
}
