using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using School.WEB.Data.Repository;
using School.WEB.Extensions;
using School.WEB.Models;
using School.WEB.ViewModels.Account;

namespace School.WEB.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly IAuthRepository _authRepository;

        public AccountController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpGet("[action]")]
        public IActionResult Login()
        {
            var model = new LoginModel();

            if (TempData["Result"] != null)
            {
                model.OperationResult = TempData.Get<OperationResult>("Result");
            }

            return View(model);
        }

        [HttpPost("[action]")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _authRepository.Get(
                    model.Email,
                    model.Password);

                if (user != null)
                {
                    await Authenticate(model.Email);

                    TempData.Put(
                        "Result",
                        new OperationResult(
                            true,
                            $"User {model.Email} was sign in system at {DateTime.Now.ToShortTimeString()}"));

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(
                    "",
                    "Incorrect login or password");

                return View(model);
            }

            return View(model);
        }

        [HttpGet("[action]")]
        public IActionResult Register()
        {
            if (TempData["Result"] != null)
            {
                ViewBag.Result = TempData.Get<OperationResult>("Result");
            }

            return View();
        }

        [HttpPost("[action]")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _authRepository.Get(
                    model.Email,
                    model.Password);

                if (user == null)
                    try
                    {
                        await _authRepository.Add(new User
                        {
                            Email = model.Email,
                            Password = model.Password
                        });

                        await _authRepository.SaveChanges();

                        await Authenticate(model.Email);

                        TempData.Put(
                            "Result",
                            new OperationResult(
                                true,
                                $"User {model.Email} was added at {DateTime.Now.ToShortTimeString()}"));

                        return RedirectToAction("Index", "Home");
                    }
                    catch (DbUpdateException)
                    {
                        _authRepository.CleanLocal();

                        TempData.Put(
                            "Result",
                            new OperationResult(
                                false,
                                $"Email {model.Email} is already registered"));

                        return RedirectToAction("Register", "Account");
                    }

                ModelState.AddModelError(
                    "",
                    "Incorrect login or password");

                return View(model);
            }

            return View(model);
        }

        [HttpGet("[action]")]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var user = await _authRepository.GetWhenForgotPassword(email);

            if (user == null)
            {
                TempData.Put(
                    "Result",
                    new OperationResult(
                        false,
                        $"We couldn't find your account, you may have made a mistake in your email"));

                return RedirectToAction("Index", "Home");
            }

            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(
                "Admin",
                "nazarii.fisenko@gmail.com"));

            emailMessage.To.Add(new MailboxAddress(
                "User",
                email));

            emailMessage.Subject = "ContactUs";

            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = $"Please, don't forget password. You password is {user.Password}"
            };

            using var client = new SmtpClient();

            await client.ConnectAsync(
                "smtp.gmail.com",
                587,
                false);

            await client.AuthenticateAsync(
                "nazarii.fisenko@gmail.com",
                await GetPassword());

            await client.SendAsync(emailMessage);

            await client.DisconnectAsync(true);

            TempData.Put(
                "Result",
                new OperationResult(
                    true,
                    $"The password has been sent to your e-mail"));

            return RedirectToAction("Login", "Account");
        }

        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };

            var id = new ClaimsIdentity(
                claims,
                "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(id));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Account");
        }

        [Authorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> ChangePassword()
        {
            var model = new ChangePasswordModel
            {
                Email = User.Identity.Name
            };

            if (TempData["Result"] != null)
            {
                model.OperationResult = TempData.Get<OperationResult>("Result");
            }

            return View(model);
        }

        [Authorize]
        [HttpPost("[action]")]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _authRepository.Get(model.Email, model.OldPassword);

                if (user != null)
                {
                    user.Password = model.NewPassword;

                    _authRepository.Update(user);

                    await _authRepository.SaveChanges();

                    TempData.Put(
                        "Result",
                        new OperationResult(
                            true,
                            $"Password was changed"));

                    return RedirectToAction("Index", "Home");
                }

                TempData.Put(
                    "Result",
                    new OperationResult(
                        false,
                        $"Password wasn't changed. You entered wrong old password!"));

                return RedirectToAction("ChangePassword", "Account");
            }

            return View();
        }

        private async Task<string> GetPassword()
        {
            const string path = @"password.txt";

            using var sr = new StreamReader(path);

            return await sr.ReadToEndAsync();
        }
    }
}