using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace School.WEB.Controllers
{
    [Authorize]
    [Route("Manage")]
    public class AdminController : Controller
    {
        public AdminController()
        {
        }

        [HttpGet("[action]")]
        public IActionResult Index()
        {
            return View();
        }
    }
}