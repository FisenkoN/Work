using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using School.BLL.Services;
using School.DAL;
using School.DAL.Entities;
using School.WEB.Models;

namespace School.WEB.Controllers
{
    public class AccountController : Controller
    {
        private readonly SchoolDbContext _db;

        public AccountController(MainService mainService)
        {
            _db = mainService.DbContext();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _db.Users.FirstOrDefaultAsync(u =>
                u.Email == model.Email && u.Password == model.Password);
            
            if (user != null)
            {
                await Authenticate(model.Email);

                return RedirectToAction("Index",
                    "Home");
            }

            ModelState.AddModelError("",
                "Incorrect login or password");

            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            
            if (user == null)
            {
                _db.Users.Add(new User
                {
                    Email = model.Email,
                    Password = model.Password
                });
                
                await _db.SaveChangesAsync();

                await Authenticate(model.Email);

                return RedirectToAction("Index",
                    "Home");
            }
            else
            {
                ModelState.AddModelError("",
                    "Incorrect login or password");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult ForgotPassword(string email)
        {
            var password = _db.Users.FirstOrDefault(u => u.Email == email)?.Password;
            
            var emailMessage = new MimeMessage();
            
            emailMessage.From.Add(new MailboxAddress("Admin",
                "nazarii.fisenko@gmail.com"));
            
            emailMessage.To.Add(new MailboxAddress("User",
                email));
            
            emailMessage.Subject = "ContactUs";

            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = $"Please, don't forget password. You password is {password}"
            };
            
            using var client = new SmtpClient();
            
            client.Connect("smtp.gmail.com",
                587,
                false);
            
            client.Authenticate("nazarii.fisenko@gmail.com",
                "[password]");
            
            client.Send(emailMessage);
            
            client.Disconnect(true);
            
            return RedirectToAction("Login");
        }

        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType,
                    userName)
            };

            var id = new ClaimsIdentity(claims,
                "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login",
                "Account");
        }
    }
}