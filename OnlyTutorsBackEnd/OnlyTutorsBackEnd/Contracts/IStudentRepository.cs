using OnlyTutorsBackEnd.Models;
using OnlyTutorsBackEnd.ModelsViews;

namespace OnlyTutorsBackEnd.Contracts
{
    public interface IStudentRepository
    {
        public Task<IEnumerable<Student>> GetStudents();
        public Task<Student> GetStudentById(int userId);
        public Task<IEnumerable<ViewStudent>> GetStudentsByLesson(int lessonId);
        public Task<int> InsertStudent(Student student);
        public Task<int> UpdateStudent(UpdateStudent student, int studentid);
    }
}
