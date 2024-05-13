using KursovaBack.Data;
using KursovaBack.ViewModels;
using System.Security.Claims;

namespace KursovaBack.Services.Interfaces
{
    public interface IAccountService
    {
        Task<BaseResponse<ClaimsIdentity>> Register(UserRegisterVM model);

        Task<BaseResponse<ClaimsIdentity>> Login(UserLoginVM model);

  
    }
}
