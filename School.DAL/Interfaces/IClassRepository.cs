using School.DAL.Entities;

namespace School.DAL.Interfaces
{
    public interface IClassRepository : IRepository<Class>,
        IRelated<Class, Teacher>
    {

    }
}