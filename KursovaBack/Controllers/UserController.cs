using KursovaBack.DatabaseAccess.Interfaces;
using KursovaBack.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

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
        public async Task<List<User>> GetAll()
        {
            var users= await _userRepository.GetAll();
            foreach (var user in users)
            {
                try
                {
                    if (user.Avatar != null)
                    {
                        var imageBase64 = user.Avatar != null ? Convert.ToBase64String(user.Avatar) : null;
                        user.ImageBase64 = imageBase64;
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return users;
        }
        [Route("id/{id}")]
        [HttpGet]
        public User GetUser(Guid id)
        {
            User user = new User();
            if (id != Guid.Empty)
            {
                 user = _userRepository.Get(id);
                try
                {
                    if (user.Avatar != null)
                    {
                        var imageBase64 = user.Avatar != null ? Convert.ToBase64String(user.Avatar) : null;
                        user.ImageBase64 = imageBase64;
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return user;
        }
        [Route("Update")]
        [HttpPost]
        public async void Update([FromForm] User user)
        {
            if(user != null) {
                byte[] imageData = null;
                if (user.fromFile != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        await user.fromFile.CopyToAsync(ms);
                        imageData = ms.ToArray();
                    }
                }
                user.Avatar = imageData;
                _userRepository.Update(user);
            }
        }
        [Route("Name")]
        [HttpGet]
        public List<User> GetUserByName(string namePrefix)
        {
            return _userRepository.GetByNamePrefix(namePrefix);
        }
        
        

    }
}
