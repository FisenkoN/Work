using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace School.WEB.Data.Repository
{
    public interface IRelated<T, Y>
    {
        public IIncludableQueryable<T, Y> GetRelatedData();

        public Task<T> GetOneRelated(int? id);
    }
}