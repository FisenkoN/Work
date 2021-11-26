using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        private readonly IRoleRepository _roleRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IClassRepository _classRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly ITeacherRepository _teacherRepository;

        public AccountController(
            IAuthRepository authRepository, 
            IRoleRepository roleRepository,
            IStudentRepository studentRepository,
            ISubjectRepository subjectRepository,
            ITeacherRepository teacherRepository,
            IClassRepository classRepository)
        {
            _authRepository = authRepository;
            _roleRepository = roleRepository;
            _studentRepository = studentRepository;
            _studentRepository = studentRepository;
            _studentRepository = studentRepository;
            _classRepository = classRepository;
            _teacherRepository = teacherRepository;
            _subjectRepository = subjectRepository;
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
                    await Authenticate(await _authRepository.Get(model.Email, model.Password));
                    
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
                            Password = model.Password,
                            Role = await _roleRepository.Get("student")
                        });
                        
                        await _authRepository.SaveChanges();

                        await Authenticate(await _authRepository.Get(model.Email, model.Password));

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
            var user = await _authRepository.Get(email);

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

        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name)
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
            var model = new ChangePasswordModel { Email = User.Identity.Name };
            
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

        [Authorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> Profile()
        {
            var user = await _authRepository.Get(User.Identity?.Name);

            if (user.StudentId != null)
                return RedirectToAction("StudentProfile", "Account", new {id = user.StudentId});
            
            if (user.TeacherId != null)
                return RedirectToAction("StudentProfile", "Account", new {id = user.StudentId});
            
            if (user.AdminId != null) 
                return RedirectToAction("StudentProfile", "Account", new {id = user.StudentId});

            TempData.Put(
                "Result",
                new OperationResult(
                    false,
                    $"This user is not synchronized with any account"));

            return RedirectToAction("Index", "Home");
        }
        
        [Authorize(Roles = "student")]
        [HttpGet("[action]")]
        public async Task<IActionResult> StudentProfile(int id)
        {
            var student = await _studentRepository.GetOneRelated(id);

            var className = (await _classRepository.GetOne(student.ClassId))?.Name ?? "no class";

            var subjectNames = student.Subjects.Select(s => s.Name);

            var user = await _authRepository.Get(student.UserId.Value);

            var role = await _roleRepository.Get(user.RoleId.Value);

            var model = new StudentProfileModel(student, className, subjectNames, user, role);

            return View(model);
        }
        
        [Authorize(Roles = "student")]
        [HttpGet("[action]")]
        public async Task<IActionResult> AdminProfile(int id)
        {
            var student = await _studentRepository.GetOneRelated(id);

            var className = (await _classRepository.GetOne(student.ClassId))?.Name ?? "no class";

            var subjectNames = student.Subjects.Select(s => s.Name);

            var user = await _authRepository.Get(student.UserId.Value);

            var role = await _roleRepository.Get(user.RoleId.Value);

            var model = new StudentProfileModel(student, className, subjectNames, user, role);

            return View(model);
        }
    }
}