using System.Collections.Generic;
using School.DAL.Entities;

namespace School.DAL.Interfaces
{
    public interface ISubjectRepository : IRepository<Subject>,
        IRelated<Subject, ICollection<Teacher>>
    {
    }
}