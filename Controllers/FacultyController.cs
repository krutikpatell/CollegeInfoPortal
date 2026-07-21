using CollegeInfoPortal.Data;
using CollegeInfoPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CollegeInfoPortal.Controllers
{
    public class FacultyController : Controller
    {
        private readonly CollegeDbContext _context;

        public FacultyController(CollegeDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var faculty = await _context.Faculty.Include(f => f.Department).ToListAsync();
            return View(faculty);
        }

        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Designation,Email,DepartmentId")] Faculty faculty)
        {
            if (ModelState.IsValid)
            {
                _context.Add(faculty);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Faculty created";
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name", faculty.DepartmentId);
            return View(faculty);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var faculty = await _context.Faculty.FindAsync(id);
            if (faculty == null) return NotFound();
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name", faculty.DepartmentId);
            return View(faculty);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FacultyId,Name,Designation,Email,DepartmentId")] Faculty faculty)
        {
            if (id != faculty.FacultyId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(faculty);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "Faculty updated";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Faculty.Any(e => e.FacultyId == faculty.FacultyId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name", faculty.DepartmentId);
            return View(faculty);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var faculty = await _context.Faculty.Include(f => f.Department).FirstOrDefaultAsync(f => f.FacultyId == id);
            if (faculty == null) return NotFound();
            return View(faculty);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var faculty = await _context.Faculty.FindAsync(id);
            if (faculty != null)
            {
                _context.Faculty.Remove(faculty);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Faculty deleted";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
