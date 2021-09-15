using School.DAL.Entities;

namespace School.DAL.Interfaces
{
    public interface ITeacherRepository : IRepository<Teacher>,
        IRelated<Teacher, Class>
    {

    }
}