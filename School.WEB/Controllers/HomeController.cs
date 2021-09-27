using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using School.BLL.Services;
using School.WEB.Models;

namespace School.WEB.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(MainService mainService)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ContactUs(Contact contact)
        {
            var emailService = new EmailService();

            emailService.SendEmailAsync(contact.Email,
                contact.PhoneNumber,
                contact.Message,
                contact.Email);

            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0,
            Location = ResponseCacheLocation.None,
            NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}