using KursovaBack.Data;
using KursovaBack.DatabaseAccess.Interfaces;
using KursovaBack.Models;
using KursovaBack.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using KursovaBack.Services.Interfaces;
using KursovaBack.DatabaseAccess.Repositories;

namespace KursovaBack.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AccountService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<BaseResponse<ClaimsIdentity>> Login(UserLoginVM model)
        {
            try
            {
                User user = _userRepository.GetByName(model.Username);
                if (user != null)
                {
                 
                    if (user.password != model.Password)
                    {
                        return new BaseResponse<ClaimsIdentity>()
                        {
                            Description = "Incorrect password"
                        };
                    }
                    var jwtToken = GenerateJwtToken(user);

                    return new BaseResponse<ClaimsIdentity>()
                    {
                        StatusCode = StatusCode.OK,
                        JwtToken = jwtToken,
                    };
                }
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = "User not found"
                };

            }
            catch (Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        async Task<BaseResponse<ClaimsIdentity>> IAccountService.Register(UserRegisterVM model)
        {
           // Task < BaseResponse < ClaimsIdentity >> result = null;
            try
            {
                User user = _userRepository.GetByName(model.Username);
                if (user == null)
                {

                    user = new User()
                    {
                           
                            username = model.Username,
                            password = model.Password,
                            id = Guid.NewGuid(),
                            lastname = model.LastName,
                            firstname = model.FirstName,
                            role = model.Role,

                        };
                        _userRepository.Create(user);
                        var jwtToken = GenerateJwtToken(user);
                        return new BaseResponse<ClaimsIdentity>()
                        {
                            Description = "Added",
                            StatusCode = StatusCode.OK,
                            JwtToken = jwtToken
                           
                        };
                }
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = "User with this email is already exists!",
                };

            }
            catch (Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
            
           
        }
        private string GenerateJwtToken(User user)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));

            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim("Id", user.id.ToString()),
                new Claim(ClaimTypes.Name, user.username),
                new Claim(ClaimTypes.Role, user.role.ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(2),
                Issuer = "http://localhost:54144",
                Audience = "http://localhost:54144",
                SigningCredentials = credentials
            };
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);

            return jwt;
        }
    }
}
