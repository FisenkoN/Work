using Microsoft.EntityFrameworkCore.Query;

namespace School.DAL.Interfaces
{
    public interface IRelated<T, Y>
    {
        public IIncludableQueryable<T, Y> GetRelatedData();

        public T GetOneRelated(int? id);
    }
}