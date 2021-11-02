using School.WEB.Models;

namespace School.WEB.Data.Repository
{
    public interface ITeacherRepository : IBaseRepository<Teacher>,
        IRelated<Teacher, Class>
    {
    }
}