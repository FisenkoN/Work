using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using School.BLL.Dto;
using School.BLL.Services;

namespace School.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly BlogServices _service;

        public BlogController(MainService service)
        {
            _service = new BlogServices(service);
        }

        [HttpGet]
        // GET: api/Blog/
        public ActionResult<IEnumerable<BlogDto>> GetBlogs()
        {
            return new ActionResult<IEnumerable<BlogDto>>(_service.GetBlogs());
        }

        // GET: api/Blog/1
        [HttpGet("{id:int}")]
        public ActionResult<BlogDto> GetBlogs(int? id)
        {
            return new ActionResult<BlogDto>(_service.GetBlogForId(id));
        }

        // POST: api/Blog/
        [HttpPost]
        public ActionResult<BlogDto> GetBlogs(BlogDto blogDto)
        {
            _service.Create(blogDto);

            return CreatedAtAction("GetBlogs",
                new
                {
                    id = blogDto.Id
                },
                blogDto);
        }

        // PUT: api/Blog/5
        [HttpPut("{id:int}")]
        public IActionResult PutSubject(int id,
            BlogDto blog)
        {
            if (id != blog.Id)
                return BadRequest();

            try
            {
                _service.Edit(id,
                    blog);
            }
            catch (Exception)
            {
                if (!BlogExists(id))
                    return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Blog/5
        [HttpDelete("{id:int}")]
        public IActionResult DeleteSubject(int id)
        {
            var blog = _service.GetBlogForId(id);

            if (blog == null)
                return NotFound();

            _service.Delete(id);

            return NoContent();
        }

        private bool BlogExists(int id)
        {
            return _service.GetBlogs()
                .Any(e =>
                    e.Id == id);
        }
    }
}