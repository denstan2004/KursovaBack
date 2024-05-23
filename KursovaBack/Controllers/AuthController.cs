using KursovaBack.Services.Interfaces;
using KursovaBack.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KursovaBack.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAccountService _accountService;

        public AuthController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserRegisterVM model)
        {
            if (ModelState.IsValid)
            {
               
                var response = await _accountService.Register(model);
                if (response.StatusCode == Data.StatusCode.OK)
                {

                    Response.Cookies.Append("JwtToken", response.JwtToken, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        Expires = DateTimeOffset.Now.AddHours(2)
                    });

                    return Ok(new
                    {
                        JwtToken = response.JwtToken,
                     
                    });
                }
                ModelState.AddModelError("", response.Description);
            }
            return BadRequest(ModelState);

        }
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLoginVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _accountService.Login(model);
                if (response.StatusCode == Data.StatusCode.OK)
                {
                    Response.Cookies.Append("JwtToken", response.JwtToken, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        Expires = DateTimeOffset.Now.AddHours(2)
                    });

                    return Ok(new
                    {
                        JwtToken = response.JwtToken,
                    });
                }
                ModelState.AddModelError("", response.Description);
            }
            return BadRequest(ModelState);
        }
        [HttpGet]
        [Route("test")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult TestGet()
        {
            return Ok("Get is working!!");
        }
    }
}
