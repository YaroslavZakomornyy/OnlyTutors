using Dapper;
using OnlyTutorsBackEnd.Contracts;
using OnlyTutorsBackEnd.Models;
using OnlyTutorsBackEnd.ModelsViews;
using System.Data;

namespace OnlyTutorsBackEnd.Repositories
{
    public class TutorRepository : ITutorRepository
    {
        private DapperContext _context;
        private IUserRepository _userRepository;

        public TutorRepository(DapperContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;   
        }

        public async Task<IEnumerable<Tutor>> GetTutors()
        {
            try
            {
                string query = "SELECT * FROM Tutors JOIN Users ON Tutors.userid = Users.id";

                using (var connection = _context.CreateConnection())
                {
                    var tutors = await connection.QueryAsync<Tutor>(query);
                    return tutors.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message + "\n" + ex.StackTrace);
                return Enumerable.Empty<Tutor>();
            }
        }

        public async Task<Tutor> GetTutorById(int userId)
        {
            try
            {
                string query = "SELECT * FROM Tutors JOIN Users ON Tutors.userid = Users.id WHERE userId = @userId";

                var parameters = new DynamicParameters();
                parameters.Add("userId", userId, DbType.Int32);

                using (var connection = _context.CreateConnection())
                {
                    var tutor = (await connection.QueryAsync<Tutor>(query, parameters)).First();
                    return tutor;
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message + "\n" + ex.StackTrace);
                return null;
            }
        }

        public async Task<IEnumerable<Tutor>> SearchForTutors(string searchString)
        {
            try
            {
                string query = "SELECT * FROM GetTutorByName(@search) UNION SELECT * FROM GetTutorBySubject(@search);";

                var parameters = new DynamicParameters();
                parameters.Add("search", searchString, DbType.String);

                using (var connection = _context.CreateConnection())
                {
                    var tutors = await connection.QueryAsync<Tutor>(query,parameters);
                    return tutors.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message + "\n" + ex.StackTrace);
                return Enumerable.Empty<Tutor>();
            }
        }

        public async Task<int> InsertTutor(Tutor tutor)
        {
            try
            {
                int userId = await _userRepository.InsertUser(tutor);

                if (userId == -1)
                    throw new Exception("Error while creating user during tutor creation");

                string query = "INSERT INTO Tutors (userId, description, experience) VALUES (@userId, @description, @experience)";

                var parameters = new DynamicParameters();
                parameters.Add("userId", userId, DbType.Int32);
                parameters.Add("description", tutor.Description, DbType.String);
                parameters.Add("experience", tutor.Experience, DbType.String);

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

        public async Task<int> UpdateTutor(UpdateTutor tutor, int userId)
        {
            try
            {
                int updateResult = await _userRepository.UpdateUser(tutor, userId);

                if (updateResult == -1)
                    throw new Exception("Error while updating user during tutor update");

                string query = "UPDATE Tutors Set description=@description, experience=@experience WHERE userId = @userId";

                var parameters = new DynamicParameters();
                parameters.Add("userId", userId, DbType.Int32);
                parameters.Add("description", tutor.Description, DbType.String);
                parameters.Add("experience", tutor.Experience, DbType.String);

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
