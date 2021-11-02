using School.WEB.Models;

namespace School.WEB.Data.Repository
{
    public interface IClassRepository : IBaseRepository<Class>,
        IRelated<Class, Teacher>
    {
    }
}