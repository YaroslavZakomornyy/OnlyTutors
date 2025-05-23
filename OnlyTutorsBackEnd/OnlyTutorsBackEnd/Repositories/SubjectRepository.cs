using Dapper;
using OnlyTutorsBackEnd.Contracts;
using OnlyTutorsBackEnd.Models;
using System.Data;

namespace OnlyTutorsBackEnd.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private DapperContext _context;

        public SubjectRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Subject>> GetSubjects()
        {
            try
            {
                string query = "SELECT * FROM Subjects";

                using (var connection = _context.CreateConnection())
                {
                    var subjects = await connection.QueryAsync<Subject>(query);
                    return subjects.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message + "\n" + ex.StackTrace);
                return Enumerable.Empty<Subject>();
            }
        }

        public async Task<Subject> GetSubjectByLessonId(int lessonid)
        {
            try
            {
                string query = "SELECT Subjects.id, Subjects.name, Subjects.complexity FROM Subjects JOIN Lessons ON Subjects.id = Lessons.subjectid WHERE Lessons.id = @lessonid";

                var parameters = new DynamicParameters();
                parameters.Add("lessonid", lessonid, DbType.Int32);

                using (var connection = _context.CreateConnection())
                {
                    var subject = (await connection.QueryAsync<Subject>(query, parameters)).First();
                    return subject;
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message + "\n" + ex.StackTrace);
                return null;
            }
        }

        public async Task<int> InsertSubject(Subject subject)
        {
            try
            {
                string query = "INSERT INTO Subjects (name, complexity) VALUES (@name, @complexity)";

                var parameters = new DynamicParameters();
                parameters.Add("name", subject.Name, DbType.String);
                parameters.Add("complexity", subject.Complexity, DbType.Decimal);

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
