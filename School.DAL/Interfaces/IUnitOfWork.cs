namespace School.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        public IStudentRepository Students { get; }
        
        public ITeacherRepository Teachers { get; }
        
        public IClassRepository Classes { get; }
        
        public ISubjectRepository Subjects { get; }

        void Save();
    }
}