using School.WEB.Models;

namespace School.WEB.Data.Repository
{
    public interface IStudentRepository : IBaseRepository<Student>,
        IRelated<Student, Class>
    {
    }
}