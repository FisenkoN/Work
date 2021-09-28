using System.Collections.Generic;
using System.Linq;
using School.BLL.Dto;
using School.BLL.Mapper;
using School.DAL.Interfaces;

namespace School.BLL.Services
{
    public class BlogServices
    {
        private readonly Map _map;

        private readonly IUnitOfWork _unitOfWork;

        public BlogServices(MainService mainService)
        {
            _unitOfWork = mainService.UnitOfWork();

            _map = new Map(_unitOfWork);
        }

        public IEnumerable<BlogDto> GetBlogs() =>
            from b in _unitOfWork
                .Blogs
                .GetAll()
            select _map.To(b);

        public BlogDto GetBlogForId(int? id) =>
            _map.To(_unitOfWork.Blogs.GetOne(id));

        public void Delete(int? id)
        {
            if (id != null)
                _unitOfWork
                    .Blogs
                    .Delete(id.Value);
        }
        
        public void Create(BlogDto blog) =>
            _unitOfWork
                .Blogs
                .Add(_map.To(blog));
        
        public void Edit(int? id, BlogDto blogDto)
        {
            var blog = _unitOfWork
                .Blogs
                .GetOne(id);

            blog.Name = blogDto.Name;
            blog.Id = blogDto.Id;
            blog.Image = blogDto.Image;
            blog.Text = blogDto.Text;
            blog.Category = blogDto.Category;

            _unitOfWork
                .Blogs
                .Update(blog);
        }
    }
}