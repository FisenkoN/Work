using School.DAL;
using School.DAL.DataInitialization;
using School.DAL.Interfaces;
using School.DAL.Repository;

namespace School.BLL.Services
{
    public class MainService
    {
        private readonly SchoolDbContext _context;

        public MainService()
        {
            _context = new SchoolDbContext();

            MyDataInitializer.RecreateDatabase(_context);
            MyDataInitializer.InitializeData(_context);
        }

        public IUnitOfWork UnitOfWork()
        {
            return new EfUnitOfWork(_context);
        }
    }
}