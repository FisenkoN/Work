using System.Collections.Generic;
using School.WEB.Models;

namespace School.WEB.Data.Repository
{
    public interface ISubjectRepository : IBaseRepository<Subject>, IRelated<Subject, ICollection<Teacher>>
    {
    }
}