using Dapper;
using OnlyTutorsBackEnd.Contracts;
using OnlyTutorsBackEnd.Models;
using OnlyTutorsBackEnd.ModelsViews;
using System.Data;

namespace OnlyTutorsBackEnd.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private DapperContext _context;
        private UserRepository _userRepository;

        public StudentRepository(DapperContext context)
        {
            _context = context;
            _userRepository = new UserRepository(context);
        }

        public async Task<Student> GetStudentById(int userId)
        {
            try
            {
                string query = "SELECT * FROM Students JOIN Users ON Students.userid = Users.id WHERE userId = @userId";

                var parameters = new DynamicParameters();
                parameters.Add("userId", userId, DbType.Int32);

                using (var connection = _context.CreateConnection())
                {
                    var student = (await connection.QueryAsync<Student>(query, parameters)).First();
                    return student;
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message + "\n" + ex.StackTrace);
                return null;
            }
        }

        public async Task<IEnumerable<Student>> GetStudents()
        {
            try
            {
                string query = "SELECT * FROM Students JOIN Users ON Students.userid = Users.id";

                using (var connection = _context.CreateConnection())
                {
                    var students = await connection.QueryAsync<Student>(query);
                    return students.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message + "\n" + ex.StackTrace);
                return Enumerable.Empty<Student>();
            }
        }

        public async Task<IEnumerable<ViewStudent>> GetStudentsByLesson(int lessonId)
        {
            try
            {
                //Invoking stored procdure on database
                string query = "SELECT * FROM GetStudents(@lessonId);";

                var parameters = new DynamicParameters();
                parameters.Add("lessonId", lessonId, DbType.Int32);

                using (var connection = _context.CreateConnection())
                {
                    var students = await connection.QueryAsync<ViewStudent>(query, parameters);
                    return students.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message + "\n" + ex.StackTrace);
                return Enumerable.Empty<ViewStudent>();
            }
        }

        public async Task<int> InsertStudent(Student student)
        {
            try
            {
                int userId = await _userRepository.InsertUser(student);

                if (userId == -1)
                    throw new Exception("Error while creating user during student creation");

                string query = "INSERT INTO Students (userId, HighestLevelOfEducation) VALUES (@userId, @HighestLevelOfEducation)";
                
                var parameters = new DynamicParameters();
                parameters.Add("userId", userId, DbType.Int32);
                parameters.Add("HighestLevelOfEducation", student.HighestLevelOfEducation, DbType.String);

                using (var connection = _context.CreateConnection())
                {
                    if (await connection.ExecuteAsync(query, parameters) > 0)
                        return 1;
                    else
                        return -1;
                }

            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message + "\n" + ex.StackTrace);
                return -1;
            }
        }

        public async Task<int> UpdateStudent(UpdateStudent student, int userid)
        {
            try
            {
                int updateResult = await _userRepository.UpdateUser(student, userid);

                if (updateResult == -1)
                    throw new Exception("Error while updating user during student update");

                string query = "UPDATE Students Set HighestLevelOfEducation=@HighestLevelOfEducation WHERE userid = @userid";

                var parameters = new DynamicParameters();
                parameters.Add("userid", userid, DbType.Int32);
                parameters.Add("HighestLevelOfEducation", student.HighestLevelOfEducation, DbType.String);
                
                using (var connection = _context.CreateConnection())
                {
                    if (await connection.ExecuteAsync(query, parameters) > 0)
                        return 1;
                    else
                        return -1;
                }

            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message + "\n" + ex.StackTrace);
                return -1;
            }
        }
    }
}
