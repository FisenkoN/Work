using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using School.WEB.Data;
using School.WEB.Extensions;
using School.WEB.ViewModels;

namespace School.WEB.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(SchoolDbContext dbContext)
        {
        }

        public IActionResult Index()
        {
            if (TempData["Result"] != null)
            {
                ViewBag.Result = TempData.Get<OperationResult>("Result");
            }

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
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}