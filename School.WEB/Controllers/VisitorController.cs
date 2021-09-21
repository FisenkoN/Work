using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using School.BLL.Dto;

namespace School.WEB.Controllers
{
    public class VisitorController : Controller
    {
        private readonly string _baseUrl;

        public VisitorController(IConfiguration configuration)
        {
            _baseUrl = configuration.GetSection("ServiceVisitorAddress")
                .Value;
        }

        // GET
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetClasses()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(_baseUrl + "/Class");

            if (response.IsSuccessStatusCode)
                return View(
                    JsonConvert.DeserializeObject<List<ClassDto>>(
                        await response.Content.ReadAsStringAsync()));

            return NotFound();
        }

        public async Task<IActionResult> GetTeachers()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(_baseUrl + "/Teacher");

            if (response.IsSuccessStatusCode)
                return View(
                    JsonConvert.DeserializeObject<List<TeacherDto>>(
                        await response.Content.ReadAsStringAsync()));

            return NotFound();
        }

        public async Task<IActionResult> GetSubjects()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(_baseUrl + "/Subject");

            if (response.IsSuccessStatusCode)
                return View(
                    JsonConvert.DeserializeObject<List<SubjectDto>>(
                        await response.Content.ReadAsStringAsync()));

            return NotFound();
        }

        public async Task<IActionResult> ClassDetails(int? id)
        {
            if (id == null)
                return BadRequest();

            var client = new HttpClient();

            var response = await client.GetAsync(_baseUrl + $"/Class/{id}");

            ClassDto @class;

            if (response.IsSuccessStatusCode)
                @class = JsonConvert.DeserializeObject<ClassDto>(await response.Content.ReadAsStringAsync());
            else
                return NotFound();

            string teacher;

            try
            {
                teacher = JsonConvert.DeserializeObject<TeacherDto>(
                        await (await client.GetAsync(
                                _baseUrl + $"/Teacher/{id}")).Content
                            .ReadAsStringAsync())
                    .FullName;
            }
            catch (Exception)
            {
                teacher = "no teacher";
            }

            var students = JsonConvert.DeserializeObject<List<string>>(
                await (await client.GetAsync(
                        _baseUrl + $"/Class/Students/{id}")).Content
                    .ReadAsStringAsync());

            return View(new Tuple<ClassDto, string, IEnumerable<string>>(
                @class,
                teacher,
                students));
        }

        public async Task<IActionResult> TeacherDetails(int? id)
        {
            if (id == null)
                return BadRequest();

            var client = new HttpClient();

            TeacherDto teacher = null;
            try
            {
                teacher = JsonConvert.DeserializeObject<TeacherDto>(
                    await (await client.GetAsync(
                            _baseUrl + $"/Teacher/{id}")).Content
                        .ReadAsStringAsync());
            }
            catch (Exception)
            {
                NotFound();
            }

            string @class;

            try
            {
                @class = JsonConvert.DeserializeObject<string>(
                    await (await client.GetAsync(
                            _baseUrl + $"/GetTeacherClass/{id}")).Content
                        .ReadAsStringAsync());
            }
            catch (Exception)
            {
                @class = null;
            }

            var subjects = JsonConvert.DeserializeObject<IEnumerable<string>>(
                await (await client.GetAsync(
                        _baseUrl + $"/GetSubjectsForTeacher/{id}")).Content
                    .ReadAsStringAsync());

            return View(new Tuple<TeacherDto, string, IEnumerable<string>>(
                teacher,
                @class,
                subjects));
        }

        public async Task<IActionResult> SubjectDetails(int? id)
        {
            if (id == null)
                return BadRequest();

            var client = new HttpClient();

            SubjectDto form = null;

            try
            {
                form = JsonConvert.DeserializeObject<SubjectDto>(
                    await (await client.GetAsync(
                            _baseUrl + $"/Subject/{id}")).Content
                        .ReadAsStringAsync());
            }
            catch (Exception)
            {
                NotFound();
            }

            IEnumerable<string> students;

            try
            {
                students = JsonConvert.DeserializeObject<IEnumerable<string>>(
                    await (await client.GetAsync(
                            _baseUrl + $"/StudentsForSubjectId/{id}")).Content
                        .ReadAsStringAsync());
            }
            catch (Exception)
            {
                students = null;
            }

            var subjects = JsonConvert.DeserializeObject<IEnumerable<string>>(
                await (await client.GetAsync(
                        _baseUrl + $"/TeachersForSubjectId/{id}")).Content
                    .ReadAsStringAsync());

            return View(new Tuple<SubjectDto, IEnumerable<string>, IEnumerable<string>>(
                form,
                students,
                subjects));
        }
    }
}