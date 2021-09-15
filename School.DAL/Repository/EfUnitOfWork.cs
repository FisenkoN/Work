using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using School.DAL.Interfaces;

namespace School.DAL.Repository
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private SchoolDbContext db;

        private StudentRepository _studentRepository;

        private ClassRepository _classRepository;

        private TeacherRepository _teacherRepository;

        private SubjectRepository _subjectRepository;

        public EfUnitOfWork()
        {
            db = new SchoolDbContext();
        }
        
        public EfUnitOfWork(SchoolDbContext dbContext)
        {
            db = dbContext;
        }

        public void Save()
        {
            try
            {
                db.SaveChanges();
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
            _studentRepository ??= new StudentRepository(db);

        public ITeacherRepository Teachers =>
            _teacherRepository ??= new TeacherRepository(db);

        public IClassRepository Classes =>
            _classRepository ??= new ClassRepository(db);

        public ISubjectRepository Subjects =>
            _subjectRepository ??= new SubjectRepository(db);
    }
}