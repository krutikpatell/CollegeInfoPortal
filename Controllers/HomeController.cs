using Microsoft.AspNetCore.Mvc;

namespace CollegeInfoPortal.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var hour = DateTime.Now.Hour;
            var greeting = hour < 12 ? "Good Morning" : (hour < 18 ? "Good Afternoon" : "Good Evening");
            ViewData["Greeting"] = greeting;
            ViewBag.CollegeName = "Contoso College";

            // TempData example
            TempData["Welcome"] = "Welcome to Contoso College Information Portal";

            return View();
        }

        public IActionResult About()
        {
            ViewBag.Description = "A sample College Information Portal built with ASP.NET Core MVC";
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
