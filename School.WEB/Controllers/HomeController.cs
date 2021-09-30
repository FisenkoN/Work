using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using School.BLL.Dto;
using School.BLL.Services;
using School.WEB.Models;

namespace School.WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _client;

        public HomeController()
        {
            _client = new HttpClient();
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

        [HttpGet("Blog")]
        public async Task<IActionResult> Blogs()
        {
            var response = await _client.GetAsync("https://localhost:44331/api/Blog");

            if (response.IsSuccessStatusCode)
                return View(
                    JsonConvert.DeserializeObject<List<BlogDto>>(
                        await response.Content.ReadAsStringAsync()));

            return NoContent();
        }

        [HttpGet("Blog/{id}")]
        public async Task<IActionResult> Blog(int? id)
        {
            var response = await _client.GetAsync($"https://localhost:44331/api/Blog/{id}");

            if (response.IsSuccessStatusCode)
                return View(
                    JsonConvert.DeserializeObject<BlogDto>(
                        await response.Content.ReadAsStringAsync()));

            return NotFound();
        }

        public async Task<IActionResult> News()
        {
            var _baseUrl = "https://localhost:44331/api/Admin";

            var lastUpdatedStudents = JsonConvert.DeserializeObject<IEnumerable<StudentDto>>(
                    await (await _client.GetAsync(
                            _baseUrl + $"/Student")).Content
                        .ReadAsStringAsync())
                .OrderByDescending(s => s.LastUpdatedTime)
                .Take(3);

            var lastUpdatedTeachers = JsonConvert.DeserializeObject<IEnumerable<TeacherDto>>(
                    await (await _client.GetAsync(
                            _baseUrl + $"/Teacher")).Content
                        .ReadAsStringAsync())
                .OrderByDescending(s => s.LastUpdatedTime)
                .Take(3);

            var lastUpdatedClasses = JsonConvert.DeserializeObject<IEnumerable<ClassDto>>(
                    await (await _client.GetAsync(
                            _baseUrl + $"/Class")).Content
                        .ReadAsStringAsync())
                .OrderByDescending(s => s.LastUpdatedTime)
                .Take(3);

            var lastUpdatedSubjects = JsonConvert.DeserializeObject<IEnumerable<SubjectDto>>(
                    await (await _client.GetAsync(
                            _baseUrl + $"/Subject")).Content
                        .ReadAsStringAsync())
                .OrderByDescending(s => s.LastUpdatedTime)
                .Take(3);

            ViewData["Students"] = lastUpdatedStudents;

            ViewData["Teachers"] = lastUpdatedTeachers;

            ViewData["Classes"] = lastUpdatedClasses;

            ViewData["Subjects"] = lastUpdatedSubjects;

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