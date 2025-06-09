using Application.Input.UsuarioInput;
using Application.Interfaces.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SistemaCadastroSisandAPI.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }


        [HttpGet]
        [Authorize(Policy = "Administrador")]
        public IActionResult Get()
        {
            try
            {
                var usuariosDto = _usuarioService.ObterTodosUsuariosDto();
                return Ok(usuariosDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/UsuarioPorId/{id:int}")]
        [Authorize(Policy = "Administrador")]
        public IActionResult GetUsuarioPorId([FromRoute] int id)
        {
            try
            {
                var usuarioDto = _usuarioService.ObterUsuarioDtoPorId(id);
                return Ok(usuarioDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/UsuarioPorNome/{nome}")]
        [Authorize(Policy = "Administrador")]
        public IActionResult GetUsuarioPorNome([FromRoute] string nome)
        {
            try
            {
                var usuario = _usuarioService.ObterUsuarioDtoPorNome(nome);
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("/UsuarioPadrao")]
        public IActionResult PostUsuarioPadrao([FromBody] UsuarioCadastroInput input)
        {
            try
            {
                _usuarioService.CadastrarUsuarioPadrao(input);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("/UsuarioAdministrador")]
        [Authorize(Policy = "Administrador")]
        public IActionResult PostUsuarioAdministrador([FromBody] UsuarioCadastroInput input)
        {
            try
            {
                _usuarioService.CadastrarUsuarioAdministrador(input);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Authorize(Policy = "Administrador")]
        public IActionResult Put([FromBody] UsuarioAlteracaoInput input)
        {
            try
            {
                _usuarioService.AlterarUsuario(input);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id:int}")]
        [Authorize(Policy = "Administrador")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                _usuarioService.DeletarUsuario(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
