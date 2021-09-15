using School.DAL.Entities;

namespace School.DAL.Interfaces
{
    public interface IStudentRepository : IRepository<Student>,
        IRelated<Student, Class>
    {

    }
}