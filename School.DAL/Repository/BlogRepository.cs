using School.DAL.Entities;
using School.DAL.Interfaces;

namespace School.DAL.Repository
{
    public class BlogRepository: BaseRepository<Blog>, IBlogRepository
    {
        public BlogRepository()
        {
            
        }
        
        public BlogRepository(SchoolDbContext dbContext) : base(dbContext)
        {
        }
    }
}