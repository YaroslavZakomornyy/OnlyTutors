using OnlyTutorsBackEnd.Models;
using OnlyTutorsBackEnd.ModelsViews;

namespace OnlyTutorsBackEnd.Contracts
{
    public interface IUserRepository
    {
        public Task<IEnumerable<User>> GetUsers();
        public Task<User> GetById(int id);  
        public Task<int> InsertUser(User user);
        public Task<int> UpdateUser(UpdateUser user, int userid);  
        public Task<int> RemoveUser(int userid);
        public Task<LoginResult> ValidateUserLogin(string email, string passwordHash);
    }
}