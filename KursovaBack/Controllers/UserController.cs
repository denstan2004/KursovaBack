using KursovaBack.DatabaseAccess.Interfaces;
using KursovaBack.Models;
using Microsoft.AspNetCore.Mvc;

namespace KursovaBack.Controllers
{
    [Route("api/User")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [Route("All")]
        [HttpGet]
        public Task<List<User>> GetAll()
        {
            var users= _userRepository.GetAll();
            return users;
        }
        [Route("id/{id}")]
        [HttpGet]
        public User GetUser(Guid id)
        {
            var user =_userRepository.Get(id);
            return user;
        }
        [Route("Update/{userId}")]
        [HttpPut]
        public void Update([FromBody] User user)
        {
            _userRepository.Update(user);
        }
        [Route("Name")]
        [HttpGet]
        public List<User> GetUserByName(string namePrefix)
        {
            return _userRepository.GetByNamePrefix(namePrefix);
        }
       
        

    }
}
