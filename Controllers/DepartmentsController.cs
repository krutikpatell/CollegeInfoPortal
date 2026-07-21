using CollegeInfoPortal.Data;
using CollegeInfoPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace CollegeInfoPortal.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly CollegeDbContext _context;
        private readonly IMemoryCache _cache;
        private const string DeptCacheKey = "departments_cache";

        public DepartmentsController(CollegeDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<IActionResult> Index()
        {
            if (!_cache.TryGetValue(DeptCacheKey, out List<Department> departments))
            {
                departments = await _context.Departments.AsNoTracking().ToListAsync();
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5));
                _cache.Set(DeptCacheKey, departments, cacheEntryOptions);
            }

            // Session example
            HttpContext.Session.SetString("LastVisited", DateTime.Now.ToString());

            ViewData["Title"] = "Departments";
            return View(departments);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Head,Email")] Department department)
        {
            if (ModelState.IsValid)
            {
                _context.Add(department);
                await _context.SaveChangesAsync();
                _cache.Remove(DeptCacheKey);
                // Cookie example
                Response.Cookies.Append("LastDept", department.Name);
                TempData["Message"] = "Department created successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var department = await _context.Departments.FindAsync(id);
            if (department == null) return NotFound();
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DepartmentId,Name,Head,Email")] Department department)
        {
            if (id != department.DepartmentId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(department);
                    await _context.SaveChangesAsync();
                    _cache.Remove(DeptCacheKey);
                    TempData["Message"] = "Department updated";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Departments.Any(e => e.DepartmentId == department.DepartmentId))
                        return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var department = await _context.Departments.FirstOrDefaultAsync(d => d.DepartmentId == id);
            if (department == null) return NotFound();
            return View(department);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department != null)
            {
                _context.Departments.Remove(department);
                await _context.SaveChangesAsync();
                _cache.Remove(DeptCacheKey);
                TempData["Message"] = "Department deleted";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
