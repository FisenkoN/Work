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

namespace School.WEB.Controllers
{
    public class AdminController : Controller
    {
        private readonly string _baseUrl;

        private readonly HttpClient _client;

        public AdminController(IConfiguration configuration)
        {
            _baseUrl = configuration.GetSection("ServiceAdminAddress")
                .Value;

            _client = new HttpClient();
        }
            
        // GET
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetTeachers()
        {
            if (TempData["Message"] != null)
                ViewBag.Message = TempData["Message"]
                    .ToString();

            var response = await _client.GetAsync(_baseUrl + "/Teacher");

            if (response.IsSuccessStatusCode)
                return View(
                    JsonConvert.DeserializeObject<List<TeacherDto>>(
                        await response.Content.ReadAsStringAsync()));

            return NotFound();
        }
        
        public async Task<IActionResult> GetBlogs()
        {
            if (TempData["Message"] != null)
                ViewBag.Message = TempData["Message"]
                    .ToString();

            var response = await _client.GetAsync("https://localhost:44331/api/Blog");

            if (response.IsSuccessStatusCode)
                return View(
                    JsonConvert.DeserializeObject<List<BlogDto>>(
                        await response.Content.ReadAsStringAsync()));

            return NotFound();
        }
        
        public async Task<IActionResult> DetailsBlog(int? id)
        {
            if (id == null)
                return BadRequest();

            BlogDto blog;

            try
            {
                blog = JsonConvert.DeserializeObject<BlogDto>(
                    await (await _client.GetAsync(
                             $"https://localhost:44331/api/Blog/{id}")).Content
                        .ReadAsStringAsync());
            }
            catch (Exception)
            {
                return NotFound();
            }

            return View(blog);
        }
        
        public IActionResult CreateBlog()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateBlog(
            [Bind("Name, Category, Id, Text, Image")]
            BlogDto blog)  
        {
            var json = JsonConvert.SerializeObject(blog);

            var response = await _client.PostAsync("https://localhost:44331/api/Blog",
                new StringContent(json,
                    Encoding.UTF8,
                    "application/json"));

            TempData["Message"] = $"Blog: {blog.Name} was created at {DateTime.Now.ToShortTimeString()}";

            return RedirectToAction("GetBlogs");
        }
        
         public async Task<IActionResult> EditBlog(int? id) 
        {
            if (id == null)
                return BadRequest();

            BlogDto blog;

            var response = await _client.GetAsync($"https://localhost:44331/api/Blog/{id}");

            try
            {
                blog = JsonConvert.DeserializeObject<BlogDto>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception)
            {
                return NotFound();
            }

            return View(blog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBlog(int? id,
            [Bind("Name, Category, Id, Text, Image, CreatedTime")]
            BlogDto blog)
        {
            if (id != blog.Id)
                return NotFound();

            var json = JsonConvert.SerializeObject(blog);

            var response = await _client.PutAsync($"https://localhost:44331/api/Blog/{id}",
                new StringContent(json,
                    Encoding.UTF8,
                    "application/json"));

            var b = JsonConvert.DeserializeObject<BlogDto>(
                await (await _client.GetAsync(
                        $"https://localhost:44331/api/Blog/{blog.Id}")).Content
                    .ReadAsStringAsync());

            TempData["Message"] = $"Blog: {b.Name} was edited at {DateTime.Now.ToShortTimeString()}";

            return RedirectToAction("GetBlogs");
        }
        
        public async Task<IActionResult> DeleteBlog(int? id)
        {
            if (id == null)
                return BadRequest();

            try
            {
                JsonConvert.DeserializeObject<BlogDto>(
                    await (await _client.GetAsync(
                            $"https://localhost:44331/api/Blog/{id}")).Content
                        .ReadAsStringAsync());
            }
            catch (Exception)
            {
                return NotFound();
            }

            try
            {
                JsonConvert.DeserializeObject<TeacherDto>(
                    await (await _client.DeleteAsync(
                            $"https://localhost:44331/api/Blog/{id}")).Content
                        .ReadAsStringAsync());

                TempData["Message"] = $"Blog with id : {id} was deleted at {DateTime.Now.ToShortTimeString()}";

                return RedirectToAction("GetBlogs");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> GetClasses()
        {
            if (TempData["Message"] != null)
                ViewBag.Message = TempData["Message"]
                    .ToString();

            var response = await _client.GetAsync(_baseUrl + "/Class");

            if (response.IsSuccessStatusCode)
                return View(
                    JsonConvert.DeserializeObject<List<ClassDto>>(
                        await response.Content.ReadAsStringAsync()));

            return NotFound();
        }

        public async Task<IActionResult> GetStudents()
        {
            if (TempData["Message"] != null)
                ViewBag.Message = TempData["Message"]
                    .ToString();

            var response = await _client.GetAsync(_baseUrl + "/Student");

            if (response.IsSuccessStatusCode)
                return View(
                    JsonConvert.DeserializeObject<List<StudentDto>>(
                        await response.Content.ReadAsStringAsync()));

            return NotFound();
        }

        public async Task<IActionResult> GetSubjects()
        {
            if (TempData["Message"] != null)
                ViewBag.Message = TempData["Message"]
                    .ToString();

            var response = await _client.GetAsync(_baseUrl + "/Subject");

            if (response.IsSuccessStatusCode)
                return View(
                    JsonConvert.DeserializeObject<List<SubjectDto>>(
                        await response.Content.ReadAsStringAsync()));

            return NotFound();
        }

        public async Task<IActionResult> CreateTeacher()
        {
            var subjects = JsonConvert.DeserializeObject<IEnumerable<SubjectDto>>(
                await (await _client.GetAsync(
                        _baseUrl + $"/Subject")).Content
                    .ReadAsStringAsync());

            var classes = JsonConvert.DeserializeObject<IEnumerable<ClassDto>>(
                await (await _client.GetAsync(
                        _baseUrl + $"/ClassWithoutTeacher")).Content
                    .ReadAsStringAsync());

            ViewData["Classes"] = new SelectList(
                classes,
                "Id",
                "Name");

            ViewData["Subjects"] = new SelectList(
                subjects,
                "Id",
                "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeacher(
            [Bind("FirstName, Image, LastName, ClassId, SubjectIds, Gender, Id, Age")]
            TeacherDto teacherDto)
        {
            var json = JsonConvert.SerializeObject(teacherDto);

            var response = await _client.PostAsync(_baseUrl + $"/Teacher",
                new StringContent(json,
                    Encoding.UTF8,
                    "application/json"));

            TempData["Message"] = $"Teacher: {teacherDto.FullName} was created at {DateTime.Now.ToShortTimeString()}";

            return RedirectToAction("GetTeachers");
        }

        public async Task<IActionResult> EditTeacher(int? id)
        {
            if (id == null)
                return BadRequest();

            TeacherDto teacher;

            var response = await _client.GetAsync(_baseUrl + $"/Teacher/{id}");

            try
            {
                teacher = JsonConvert.DeserializeObject<TeacherDto>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception)
            {
                return NotFound();
            }

            var classes = JsonConvert.DeserializeObject<IEnumerable<ClassDto>>(
                await (await _client.GetAsync(
                        _baseUrl + $"/ClassWithoutTeacher")).Content
                    .ReadAsStringAsync());

            try
            {
                classes = classes.Append(JsonConvert.DeserializeObject<ClassDto>(
                    await (await _client.GetAsync(
                            _baseUrl + $"/Classes_GetTeacher/{teacher.Id}")).Content
                        .ReadAsStringAsync()));
            }
            catch (Exception)
            {
                Console.WriteLine();
            }

            var subjects = JsonConvert.DeserializeObject<IEnumerable<SubjectDto>>(
                await (await _client.GetAsync(
                        _baseUrl + $"/Subject")).Content
                    .ReadAsStringAsync());

            try
            {
                var selectedClass = JsonConvert.DeserializeObject<ClassDto>(
                    await (await _client.GetAsync(
                            _baseUrl + $"/Classes_GetTeacher/{teacher.Id}")).Content
                        .ReadAsStringAsync());

                ViewData["Classes"] = new SelectList(classes,
                    "Id",
                    "Name",
                    selectedClass);
            }
            catch (Exception)
            {
                ViewData["Classes"] = new SelectList(classes,
                    "Id",
                    "Name");
            }

            ViewData["Subjects"] = new SelectList(subjects,
                "Id",
                "Name");

            return View(teacher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTeacher(int? id,
            [Bind("Id, FirstName, LastName,Image, Age, Gender, ClassId, SubjectIds")]
            TeacherDto teacher)
        {
            if (id != teacher.Id)
                return NotFound();

            var json = JsonConvert.SerializeObject(teacher);

            var response = await _client.PutAsync(_baseUrl + $"/Teacher/{id}",
                new StringContent(json,
                    Encoding.UTF8,
                    "application/json"));

            var t = JsonConvert.DeserializeObject<TeacherDto>(
                await (await _client.GetAsync(
                        _baseUrl + $"/Teacher/{teacher.Id}")).Content
                    .ReadAsStringAsync());

            TempData["Message"] = $"Teacher: {t.FullName} was edited at {DateTime.Now.ToShortTimeString()}";

            return RedirectToAction("GetTeachers");
        }

        public async Task<IActionResult> DeleteTeacher(int? id)
        {
            if (id == null)
                return BadRequest();

            try
            {
                JsonConvert.DeserializeObject<TeacherDto>(
                    await (await _client.GetAsync(
                            _baseUrl + $"/Teacher/{id}")).Content
                        .ReadAsStringAsync());
            }
            catch (Exception)
            {
                return NotFound();
            }

            try
            {
                JsonConvert.DeserializeObject<TeacherDto>(
                    await (await _client.DeleteAsync(
                            _baseUrl + $"/Teacher/{id}")).Content
                        .ReadAsStringAsync());

                TempData["Message"] = $"Teacher with id : {id} was deleted at {DateTime.Now.ToShortTimeString()}";

                return RedirectToAction("GetTeachers");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> DetailsTeacher(int? id)
        {
            if (id == null)
                return BadRequest();

            TeacherDto teacher;

            try
            {
                teacher = JsonConvert.DeserializeObject<TeacherDto>(
                    await (await _client.GetAsync(
                            _baseUrl + $"/Teacher/{id}")).Content
                        .ReadAsStringAsync());
            }
            catch (Exception)
            {
                return NotFound();
            }

            var subjects = JsonConvert.DeserializeObject<IEnumerable<string>>(
                await (await _client.GetAsync(
                        _baseUrl + $"/Teacher/GetSubjectsForId/{id}")).Content
                    .ReadAsStringAsync());

            var @class = JsonConvert.DeserializeObject<IEnumerable<ClassDto>>(
                                 await (await _client.GetAsync(
                                         _baseUrl + $"/Class")).Content
                                     .ReadAsStringAsync())
                             .FirstOrDefault(c =>
                                 c.TeacherId == teacher.Id)
                             ?.Name ??
                         "no class";

            return View((teacher, @class, subjects));
        }

        public async Task<IActionResult> CreateStudent()
        {
            var subjects = JsonConvert.DeserializeObject<IEnumerable<SubjectDto>>(
                await (await _client.GetAsync(
                        _baseUrl + $"/Subject")).Content
                    .ReadAsStringAsync());

            var classes = JsonConvert.DeserializeObject<IEnumerable<ClassDto>>(
                await (await _client.GetAsync(
                        _baseUrl + $"/Class")).Content
                    .ReadAsStringAsync());

            ViewData["Classes"] = new SelectList(classes,
                "Id",
                "Name");

            ViewData["Subjects"] = new SelectList(subjects,
                "Id",
                "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent(
            [Bind("FirstName, LastName, Image, ClassId, SubjectIds, Gender, Id, Age")]
            StudentDto student)
        {
            try
            {
                var json = JsonConvert.SerializeObject(student);

                var response = await _client.PostAsync(_baseUrl + $"/Student",
                    new StringContent(json,
                        Encoding.UTF8,
                        "application/json"));
            }
            catch (Exception)
            {
                return BadRequest("You entered wrong value. Every students must have subject");
            }

            TempData["Message"] = $"Student: {student.FullName} was created at {DateTime.Now.ToShortTimeString()}";

            return RedirectToAction("GetStudents");
        }

        public async Task<IActionResult> EditStudent(int? id)
        {
            if (id == null)
                return BadRequest();

            StudentDto student;

            try
            {
                student = JsonConvert.DeserializeObject<StudentDto>(
                    await (await _client.GetAsync(
                            _baseUrl + $"/Student/{id}")).Content
                        .ReadAsStringAsync());
            }
            catch (Exception)
            {
                return NotFound();
            }

            var classes = JsonConvert.DeserializeObject<IEnumerable<ClassDto>>(
                await (await _client.GetAsync(
                        _baseUrl + $"/Class")).Content
                    .ReadAsStringAsync());

            var subjects = JsonConvert.DeserializeObject<IEnumerable<SubjectDto>>(
                await (await _client.GetAsync(
                        _baseUrl + $"/Subject")).Content
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
        public async Task<IActionResult> EditStudent(int? id,
            [Bind("Id, FirstName, LastName,Image, Age, Gender, ClassId, SubjectIds")]
            StudentDto student)
        {
            if (id != student.Id)
                return NotFound();

            try
            {
                var json = JsonConvert.SerializeObject(student);

                var response = await _client.PutAsync(_baseUrl + $"/Student/{id}",
                    new StringContent(json,
                        Encoding.UTF8,
                        "application/json"));
            }
            catch (Exception)
            {
                return RedirectToAction("GetStudents");
            }

            TempData["Message"] = $"Student: {student.FullName} was edited at {DateTime.Now.ToShortTimeString()}";

            return RedirectToAction("GetStudents");
        }

        public async Task<IActionResult> DeleteStudent(int? id)
        {
            if (id == null)
                BadRequest();

            try
            {
                if (JsonConvert.DeserializeObject<StudentDto>(
                        await (await _client.GetAsync(
                                _baseUrl + $"/Student/{id}")).Content
                            .ReadAsStringAsync()) ==
                    null)
                    return NotFound();
            }
            catch (Exception)
            {
                return NotFound();
            }

            try
            {
                JsonConvert.DeserializeObject<StudentDto>(
                    await (await _client.DeleteAsync(
                            _baseUrl + $"/Student/{id}")).Content
                        .ReadAsStringAsync());

                TempData["Message"] =
                    $"Student: with {id} was deleted at {DateTime.Now.ToShortTimeString()}";

                return RedirectToAction("GetStudents");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> DetailsStudent(int? id)
        {
            if (id == null)
                return BadRequest();

            StudentDto student;

            try
            {
                student = JsonConvert.DeserializeObject<StudentDto>(
                    await (await _client.GetAsync(
                            _baseUrl + $"/Student/{id}")).Content
                        .ReadAsStringAsync());
            }
            catch (Exception)
            {
                return NotFound();
            }

            var subjects = JsonConvert.DeserializeObject<IEnumerable<string>>(
                await (await _client.GetAsync(
                        _baseUrl + $"/Students_GetSubjectsForId/{id}")).Content
                    .ReadAsStringAsync());

            var @class = student.ClassId != null
                ? JsonConvert.DeserializeObject<ClassDto>(
                        await (await _client.GetAsync(
                                _baseUrl + $"/Class/{student.ClassId}")).Content
                            .ReadAsStringAsync())
                    .Name
                : "no class";

            return View((student, @class, subjects));
        }

        public async Task<IActionResult> CreateClass()
        {
            var students = JsonConvert.DeserializeObject<IEnumerable<StudentDto>>(
                await (await _client.GetAsync(
                        _baseUrl + $"/Student")).Content
                    .ReadAsStringAsync());

            var teachers = JsonConvert.DeserializeObject<IEnumerable<TeacherDto>>(
                await (await _client.GetAsync(
                        _baseUrl + $"/TeachersWithoutClass")).Content
                    .ReadAsStringAsync());

            ViewData["Students"] = new SelectList(students,
                "Id",
                "FullName");

            ViewData["Teachers"] = new SelectList(teachers,
                "Id",
                "FullName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateClass(
            [Bind("Id, Name, StudentIds, TeacherId")]
            ClassDto classDto)
        {
            try
            {
                var json = JsonConvert.SerializeObject(classDto);

                var response = await _client.PostAsync(_baseUrl + $"/Class",
                    new StringContent(json,
                        Encoding.UTF8,
                        "application/json"));
            }
            catch (Exception)
            {
                return BadRequest("Every class must have one or more students");
            }

            TempData["Message"] = $"Class: {classDto.Name} was created at {DateTime.Now.ToShortTimeString()}";

            return RedirectToAction("GetClasses");
        }

        public async Task<IActionResult> CreateSubject()
        {
            var students = JsonConvert.DeserializeObject<IEnumerable<StudentDto>>(
                await (await _client.GetAsync(
                        _baseUrl + $"/Student")).Content
                    .ReadAsStringAsync());

            var teachers = JsonConvert.DeserializeObject<IEnumerable<TeacherDto>>(
                await (await _client.GetAsync(
                        _baseUrl + $"/Teacher")).Content
                    .ReadAsStringAsync());

            ViewData["Students"] = new SelectList(students,
                "Id",
                "FullName");

            ViewData["Teachers"] = new SelectList(teachers,
                "Id",
                "FullName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubject(
            [Bind("Id, Name, StudentIds, TeacherIds")]
            SubjectDto subject)
        {
            var json = JsonConvert.SerializeObject(subject);

            var response = await _client.PostAsync(_baseUrl + $"/Subject",
                new StringContent(json,
                    Encoding.UTF8,
                    "application/json"));

            TempData["Message"] = $"Subject: {subject.Name} was created at {DateTime.Now.ToShortTimeString()}";

            return RedirectToAction("GetSubjects");
        }

        public async Task<IActionResult> EditClass(int? id)
        {
            if (id == null)
                return BadRequest();

            ClassDto classDto;

            try
            {
                classDto = JsonConvert.DeserializeObject<ClassDto>(
                    await (await _client.GetAsync(
                            _baseUrl + $"/Class/{id}")).Content
                        .ReadAsStringAsync());
            }
            catch (Exception)
            {
                return NotFound();
            }

            var students = JsonConvert.DeserializeObject<IEnumerable<StudentDto>>(
                await (await _client.GetAsync(
                        _baseUrl + $"/Student")).Content
                    .ReadAsStringAsync());

            var teachers = JsonConvert.DeserializeObject<IEnumerable<TeacherDto>>(
                await (await _client.GetAsync(
                        _baseUrl + $"/TeachersWithoutClass")).Content
                    .ReadAsStringAsync());

            try
            {
                teachers = teachers.Append(JsonConvert.DeserializeObject<TeacherDto>(
                    await (await _client.GetAsync(
                            _baseUrl + $"/Teacher/{classDto.TeacherId}")).Content
                        .ReadAsStringAsync()));
            }
            catch (Exception)
            {
                Console.WriteLine();
            }

            ViewData["Students"] = new SelectList(students,
                "Id",
                "FullName");

            ViewData["Teachers"] = new SelectList(teachers,
                "Id",
                "FullName");

            return View(classDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditClass(int? id,
            [Bind("Id, Name, StudentIds, TeacherId")]
            ClassDto classDto)
        {
            if (id != classDto.Id)
                return NotFound();

            var json = JsonConvert.SerializeObject(classDto);

            var response = await _client.PutAsync(_baseUrl + $"/Class/{id}",
                new StringContent(json,
                    Encoding.UTF8,
                    "application/json"));

            TempData["Message"] = $"Class: {classDto.Name} was edited at {DateTime.Now.ToShortTimeString()}";

            return RedirectToAction("GetClasses");
        }

        public async Task<IActionResult> DeleteClass(int? id)
        {
            if (id == null)
                return BadRequest();

            try
            {
                if (JsonConvert.DeserializeObject<ClassDto>(
                        await (await _client.GetAsync(
                                _baseUrl + $"/Class/{id}")).Content
                            .ReadAsStringAsync()) ==
                    null)
                    return NotFound();
            }
            catch (Exception)
            {
                return NotFound();
            }

            JsonConvert.DeserializeObject<ClassDto>(
                await (await _client.DeleteAsync(
                        _baseUrl + $"/Class/{id}")).Content
                    .ReadAsStringAsync());

            TempData["Message"] = $"Class: with id: {id} was deleted at {DateTime.Now.ToShortTimeString()}";

            return RedirectToAction("GetClasses");
        }

        public async Task<IActionResult> DetailsClass(int? id)
        {
            if (id == null)
                return BadRequest();

            ClassDto @class;

            try
            {
                @class = JsonConvert.DeserializeObject<ClassDto>(
                    await (await _client.GetAsync(
                            _baseUrl + $"/Class/{id}")).Content
                        .ReadAsStringAsync());
            }
            catch (Exception)
            {
                return NotFound();
            }

            var students = JsonConvert.DeserializeObject<IEnumerable<string>>(
                await (await _client.GetAsync(
                        _baseUrl + $"/Classes_GetStudentsForId/{id}")).Content
                    .ReadAsStringAsync());

            var teacher = @class.TeacherId != null
                ? JsonConvert.DeserializeObject<TeacherDto>(
                        await (await _client.GetAsync(
                                _baseUrl + $"/Teacher/{@class.TeacherId}")).Content
                            .ReadAsStringAsync())
                    ?
                    .FullName
                : "no teacher";

            return View((@class, students, teacher));
        }

        public async Task<IActionResult> EditSubject(int? id)
        {
            if (id == null)
                return BadRequest();

            SubjectDto subject;

            try
            {
                subject = JsonConvert.DeserializeObject<SubjectDto>(
                    await (await _client.GetAsync(
                            _baseUrl + $"/Subject/{id}")).Content
                        .ReadAsStringAsync());
            }
            catch (Exception)
            {
                return NotFound();
            }

            return View(subject);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSubject(int? id,
            [Bind("Id, Name, StudentIds, TeacherIds")]
            SubjectDto subject)
        {
            if (id != subject.Id)
                return NotFound();

            var json = JsonConvert.SerializeObject(subject);

            var response = await _client.PutAsync(_baseUrl + $"/Subject/{id}",
                new StringContent(json,
                    Encoding.UTF8,
                    "application/json"));

            TempData["Message"] = $"Subject: {subject.Name} was edited at {DateTime.Now.ToShortTimeString()}";

            return RedirectToAction("GetSubjects");
        }

        public async Task<IActionResult> DeleteSubject(int? id)
        {
            if (id == null)
                return BadRequest();

            try
            {
                if (JsonConvert.DeserializeObject<SubjectDto>(
                        await (await _client.GetAsync(
                                _baseUrl + $"/Subject/{id}")).Content
                            .ReadAsStringAsync()) ==
                    null)
                    return NotFound();
            }
            catch (Exception)
            {
                return NotFound();
            }

            try
            {
                JsonConvert.DeserializeObject<SubjectDto>(
                    await (await _client.DeleteAsync(
                            _baseUrl + $"/Subject/{id}")).Content
                        .ReadAsStringAsync());

                TempData["Message"] = $"Subject with id: {id} was deleted at {DateTime.Now.ToShortTimeString()}";

                return RedirectToAction("GetSubjects");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> DetailsSubject(int? id)
        {
            if (id == null)
                return BadRequest();

            SubjectDto subject;

            try
            {
                subject = JsonConvert.DeserializeObject<SubjectDto>(
                    await (await _client.GetAsync(
                            _baseUrl + $"/Subject/{id}")).Content
                        .ReadAsStringAsync());
            }
            catch (Exception)
            {
                return NotFound();
            }

            var students = JsonConvert.DeserializeObject<IEnumerable<string>>(
                await (await _client.GetAsync(
                        _baseUrl + $"/Subject/GetStudentsForId/{id}")).Content
                    .ReadAsStringAsync());

            var teachers = JsonConvert.DeserializeObject<IEnumerable<string>>(
                await (await _client.GetAsync(
                        _baseUrl + $"/Subject/GetTeachersForId/{id}")).Content
                    .ReadAsStringAsync());

            return View((subject, students, teachers));
        }
    }
}