using OnlyTutorsBackEnd.Models;

namespace OnlyTutorsBackEnd.Contracts
{
    public interface ILessonRepository
    {
        public Task<IEnumerable<Lesson>> GetLessons();
        public Task<IEnumerable<Lesson>> SearchForLessons(string searchString, int openOnly);
        public Task<int> InsertLesson (Lesson lesson);
        public Task<int> InsertStudentLessons(int studentid, int lessonid);
        public Task<IEnumerable<Student>> GetAllLessonStudents(int lessonid);
        public Task<int> RemoveStudentLessons(int studentid, int lessonid);
    }
}
