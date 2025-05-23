using Microsoft.AspNetCore.Mvc;
using OnlyTutorsBackEnd.Contracts;
using OnlyTutorsBackEnd.Models;
using OnlyTutorsBackEnd.ModelsViews;

namespace OnlyTutorsBackEnd.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await _userRepository.GetUsers();
                if(users.Count() <= 0)
                    return NotFound();

                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}", Name = "GetUserById")]
        public async Task<IActionResult> GetUser(int id)
        {
            try
            {
                var user = await _userRepository.GetById(id);
                if(user == null)
                    return NotFound();
                    
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostUser(User user)
        {
            try
            {
                if (await _userRepository.InsertUser(user) == -1)
                    throw new Exception("Cannot insert user");

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutUser(UpdateUser user)
        {
            try
            {
                if (await _userRepository.UpdateUser(user, user.Id) == -1)
                    throw new Exception("Cannot update user");

                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                if (await _userRepository.RemoveUser(id) == -1)
                    throw new Exception("Cannot remove user");

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{email}/{password}", Name = "ValidateLogin")]
        public async Task<IActionResult> ValidateUserLogin(string email, string password)
        {
            try
            {
                var loginResult = await _userRepository.ValidateUserLogin(email, password);
                if (loginResult.UserId == -1)
                    throw new Exception("Wrong email or password");

                return Ok(loginResult);
            }
            catch (Exception ex)
            {
                return StatusCode(201, new LoginResult() { UserId=-1, UserType="none"});
            }
        }
    }
}
