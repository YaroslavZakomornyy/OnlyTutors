using OnlyTutorsBackEnd.Models;

namespace OnlyTutorsBackEnd.Contracts
{
    public interface ISubjectRepository
    {
        public Task<IEnumerable<Subject>> GetSubjects();
        public Task<Subject> GetSubjectByLessonId(int lessonid);
        public Task<int> InsertSubject (Subject subject);
    }
}
