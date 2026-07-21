using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeInfoPortal.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Head { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public ICollection<Course>? Courses { get; set; }
        public ICollection<Faculty>? Faculty { get; set; }
        public ICollection<Student>? Students { get; set; }
    }
}
