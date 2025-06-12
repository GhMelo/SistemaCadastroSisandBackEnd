using Application.Input.AuthInput;
using Application.Interfaces.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SistemaCadastroSisandAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] UsuarioLoginInput usuario)
        {
            var usuarioLoginToken = _authService.FazerLogin(usuario);

            if (usuarioLoginToken != string.Empty)
            {
                return Ok(usuarioLoginToken);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}

