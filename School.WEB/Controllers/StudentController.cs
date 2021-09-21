using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using School.BLL.Dto;
using School.BLL.Services;

namespace School.WEB.Controllers
{
    public class StudentController : Controller
    {
        private readonly string _baseUrl;

        private HttpClient _client;

        public StudentController(IConfiguration configuration)
        {
            _baseUrl = configuration.GetSection("ServiceStudentAddress")
                .Value;

            _client = new HttpClient();
        }

        // GET
        public async Task<IActionResult> Index()
        {
            var response = await _client.GetAsync(_baseUrl);

            if (response.IsSuccessStatusCode)
                return View(
                    JsonConvert.DeserializeObject<List<StudentDto>>(
                        await response.Content.ReadAsStringAsync()));

            return NotFound();
        }

        public async Task<IActionResult> StudentDetails(int? id)
        {
            if (id == null)
                return BadRequest();

            StudentDto student;

            var response = await _client.GetAsync(_baseUrl + $"/{id}");
            
            try
            {
                student = JsonConvert.DeserializeObject<StudentDto>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception)
            {
                return NotFound();
            }

            string form;

            try
            {
                form = JsonConvert.DeserializeObject<ClassDto>(
                    await (await _client.GetAsync(
                            _baseUrl + $"/Class/{student.ClassId}")).Content
                        .ReadAsStringAsync()).Name;
            }
            catch (Exception)
            {
                form = "no class";
            }

            var subjects = JsonConvert.DeserializeObject<IEnumerable<SubjectDto>>(
                await (await _client.GetAsync(
                        _baseUrl + $"/GetSubjects/{id}")).Content
                    .ReadAsStringAsync()).Select(s => s.Name);

            return View(new Tuple<StudentDto, string, IEnumerable<string>>(student,
                form,
                subjects));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return BadRequest();
        
            StudentDto student;
            
            var response = await _client.GetAsync(_baseUrl + $"/{id}");
        
            try
            {
                student = JsonConvert.DeserializeObject<StudentDto>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception)
            {
                return NotFound();
            }
        
            var classes = JsonConvert.DeserializeObject<IEnumerable<ClassDto>>(
                await (await _client.GetAsync(
                        _baseUrl + $"/GetClasses")).Content
                    .ReadAsStringAsync());

            var subjects = JsonConvert.DeserializeObject<IEnumerable<SubjectDto>>(
                await (await _client.GetAsync(
                        _baseUrl + $"/GetSubjects")).Content
                    .ReadAsStringAsync());
        
            ViewData["Classes"] = new SelectList(classes,
                "Id",
                "Name");
        
            ViewData["Subjects"] = new SelectList(subjects,
                "Id",
                "Name");
        
            return View(student);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id,    
            [Bind("Id, FirstName, LastName, Age, Image, Gender, ClassId, SubjectIds")]
            StudentDto student)
        {
            if (id != student.Id)
                return NotFound();

            var json = JsonConvert.SerializeObject(student);

            var response = await _client.PutAsync(_baseUrl + $"/{id}",
                new StringContent(json, Encoding.UTF8, "application/json"));
            
            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> ShowClassmates(int? id)    
        {
            if (id == null)
                return BadRequest();
        
            ICollection<StudentDto> classmates;
            
            var response = await _client.GetAsync(_baseUrl + $"/ShowClassmates/{id}");
        
            try
            {
                classmates = JsonConvert.DeserializeObject<ICollection<StudentDto>>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception)
            {
                return NoContent();
            }
        
            if (!classmates.Any())
                return NoContent();
        
            return View(classmates);
        }
        
        public async Task<IActionResult> ShowClassTeacher(int? id)
        {
            if (id == null)
                return BadRequest();
        
            TeacherDto teacher;
            
            var response = await _client.GetAsync(_baseUrl + $"/ShowClassTeacher/{id}");
        
            try
            {
                teacher = JsonConvert.DeserializeObject<TeacherDto>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception)
            {
                return NoContent();
            }
        
            if (teacher == null)
                return NoContent();
        
            return View(teacher);
        }
    }
}