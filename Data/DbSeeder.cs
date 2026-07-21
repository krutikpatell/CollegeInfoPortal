using CollegeInfoPortal.Models;

namespace CollegeInfoPortal.Data
{
    public static class DbSeeder
    {
        public static void Seed(CollegeDbContext context)
        {
            if (context.Departments.Any()) return; // already seeded

            var d1 = new Department { Name = "Computer Science", Head = "Dr. Alice Smith", Email = "cs@college.edu" };
            var d2 = new Department { Name = "Mathematics", Head = "Dr. Bob Jones", Email = "math@college.edu" };

            context.Departments.AddRange(d1, d2);
            context.SaveChanges();

            var c1 = new Course { Title = "Algorithms", Credits = 4, DepartmentId = d1.DepartmentId };
            var c2 = new Course { Title = "Operating Systems", Credits = 3, DepartmentId = d1.DepartmentId };
            var c3 = new Course { Title = "Calculus I", Credits = 4, DepartmentId = d2.DepartmentId };

            context.Courses.AddRange(c1, c2, c3);

            var f1 = new Faculty { Name = "Dr. Alice Smith", Designation = "Professor", Email = "alice.smith@college.edu", DepartmentId = d1.DepartmentId };
            var f2 = new Faculty { Name = "Dr. Carol Lee", Designation = "Associate Professor", Email = "carol.lee@college.edu", DepartmentId = d2.DepartmentId };

            context.Faculty.AddRange(f1, f2);

            var s1 = new Student { Name = "John Doe", Email = "john.doe@student.college.edu", DepartmentId = d1.DepartmentId };
            var s2 = new Student { Name = "Jane Roe", Email = "jane.roe@student.college.edu", DepartmentId = d2.DepartmentId };

            context.Students.AddRange(s1, s2);

            var e1 = new Event { Title = "Spring Festival", Description = "Annual spring gathering", Date = DateTime.Today.AddDays(15), Location = "Main Quad" };
            var e2 = new Event { Title = "Tech Talk", Description = "Guest lecture on AI", Date = DateTime.Today.AddDays(30), Location = "Auditorium" };

            context.Events.AddRange(e1, e2);

            context.SaveChanges();
        }
    }
}
