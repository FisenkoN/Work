using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using School.DAL.Interfaces;

namespace School.DAL.Repository
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private ClassRepository _classRepository;

        private StudentRepository _studentRepository;

        private SubjectRepository _subjectRepository;

        private TeacherRepository _teacherRepository;

        private BlogRepository _blogRepository;

        private readonly SchoolDbContext _db;

        public EfUnitOfWork()
        {
            _db = new SchoolDbContext();
        }

        public EfUnitOfWork(SchoolDbContext dbContext)
        {
            _db = dbContext;
        }

        public void Save()
        {
            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            catch (RetryLimitExceededException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public IStudentRepository Students =>
            _studentRepository ??= new StudentRepository(_db);

        public ITeacherRepository Teachers =>
            _teacherRepository ??= new TeacherRepository(_db);

        public IClassRepository Classes =>
            _classRepository ??= new ClassRepository(_db);

        public ISubjectRepository Subjects =>
            _subjectRepository ??= new SubjectRepository(_db);

        public IBlogRepository Blogs =>
            _blogRepository ??= new BlogRepository(_db);
    }
}