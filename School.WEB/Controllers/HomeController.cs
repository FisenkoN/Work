using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using School.WEB.Data;
using School.WEB.ViewModels;

namespace School.WEB.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(SchoolDbContext context)
        {
        }
        
        //[HttpGet("[action]")]
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet("[action]")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}