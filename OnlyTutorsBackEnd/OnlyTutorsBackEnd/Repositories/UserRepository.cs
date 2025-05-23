using Dapper;
using OnlyTutorsBackEnd.Contracts;
using OnlyTutorsBackEnd.Models;
using OnlyTutorsBackEnd.ModelsViews;
using System.Data;

namespace OnlyTutorsBackEnd.Repositories
{
    public class UserRepository : IUserRepository
    {
        private DapperContext _context;

        public UserRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            string query = "SELECT * FROM Users";

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var users = await connection.QueryAsync<User>(query);
                    return users.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message + "\n" + ex.StackTrace);
                return Enumerable.Empty<User>();
            }
        }

        public async Task<User> GetById(int id)
        {
            // creating paramethrized query to prevent sql injection
            string query = "SELECT * FROM Users WHERE Id = @id";

            var parameters = new DynamicParameters();
            parameters.Add("id", id, DbType.Int32);

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var user = (await connection.QueryAsync<User>(query, parameters)).First();
                    return user;
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message + "\n" + ex.StackTrace);
                return new User();
            }
        }

        public async Task<int> InsertUser(User user)
        {
            string query = "INSERT INTO Users (Name, Surname, Email,Password, PhoneNumber, DateOfBirth, Rating, ImagePath) " +
                "VALUES (@Name, @Surname, @Email, @Password, @PhoneNumber, @DateOfBirth, @Rating, @ImagePath)" +
                "RETURNING Id";

            var parameters = new DynamicParameters();
            parameters.Add("Name", user.Name, DbType.String);
            parameters.Add("Surname", user.Surname, DbType.String);
            parameters.Add("Email", user.Email, DbType.String);
            parameters.Add("Password", user.Password, DbType.String);
            parameters.Add("PhoneNumber", user.PhoneNumber, DbType.String);
            parameters.Add("DateOfBirth", user.DateOfBirth, DbType.Date);
            parameters.Add("Rating", user.Rating, DbType.Decimal);
            parameters.Add("ImagePath", user.ImagePath, DbType.String);
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    int insertedId = (await connection.QueryAsync<User>(query, parameters)).First().Id;

                    if (insertedId >= 0)
                        return insertedId;
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

        public async Task<int> UpdateUser(UpdateUser user, int userid)
        {
            string query = "UPDATE Users Set " +
                "Name = @Name, Surname = @Surname, PhoneNumber = @PhoneNumber " +
                "WHERE Id = @userid;";
            
            var parameters = new DynamicParameters();
            parameters.Add("Name", user.Name, DbType.String);
            parameters.Add("Surname", user.Surname, DbType.String);
            parameters.Add("PhoneNumber", user.PhoneNumber, DbType.String);
            parameters.Add("userid", userid, DbType.Int32);

            try
            {
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

        public async Task<int> RemoveUser(int userid)
        {
            string query = "DELETE FROM Users WHERE Id = @userid";

            var parameters = new DynamicParameters();
            parameters.Add("userid", userid);

            try
            {
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

        // todo: move validate password function to the database functions
        public async Task<LoginResult> ValidateUserLogin(string email, string passwordHash)
        {
            string query = "SELECT * FROM Users WHERE email = @email";

            var parameters = new DynamicParameters();
            parameters.Add("email", email);

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var result = await connection.QueryAsync<User>(query, parameters);

                    User user = new User();

                    if (result.Count() > 0)
                        user = result.First();

                    if (user.Password.Trim() == passwordHash)
                    {
                        string usertype;

                        query = "SELECT * FROM Students WHERE userid = @userid";
                        parameters = new DynamicParameters();
                        parameters.Add("userid", user.Id);

                        var results = await connection.QueryAsync<User>(query, parameters);

                        if (results.Count() > 0)
                            usertype = "student";
                        else
                            usertype = "tutor";


                        return new LoginResult { UserId = user.Id, UserType = usertype };
                    }
                    else
                    {
                        return new LoginResult { UserId = -1, UserType = "none" };
                    }
                        
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message + "\n" + ex.StackTrace);
                return new LoginResult { UserId = -1, UserType = "none" }; ;
            }
        }
    }
}
