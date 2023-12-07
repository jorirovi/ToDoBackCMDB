using Microsoft.AspNetCore.Mvc;
using todoAPI.Comunes.Clases.Contracts;
using todoAPI.Dominio.services.Login;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace todoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginContract entity)
        {
            return Ok(await _loginService.loginAsync(entity));
        }
    }
}

