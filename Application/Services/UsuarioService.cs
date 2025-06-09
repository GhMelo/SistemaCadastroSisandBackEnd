
using Application.DTOs;
using Application.Input.UsuarioInput;
using Domain.Entity;
using Domain.Interfaces.IRepository;
using Application.Interfaces.IService;

namespace Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioService(IUsuarioRepository usuarioRepository)
            => _usuarioRepository = usuarioRepository;

        public void AlterarUsuario(UsuarioAlteracaoInput usuarioAlteracaoInput)
        {
            var usuario = _usuarioRepository.ObterPorId(usuarioAlteracaoInput.Id);
            usuario.Nome = usuarioAlteracaoInput.Nome;
            usuario.Tipo = usuarioAlteracaoInput.Tipo;
            usuario.Email = usuarioAlteracaoInput.Email;
            usuario.Senha = usuarioAlteracaoInput.Senha;
            _usuarioRepository.Alterar(usuario);
        }

        public void CadastrarUsuarioAdministrador(UsuarioCadastroInput UsuarioCadastroInput)
        {
            var Usuario = new Usuario()
            {
                Nome = UsuarioCadastroInput.Nome,
                Email = UsuarioCadastroInput.Email,
                Tipo = TipoUsuario.Administrador,
                Senha = UsuarioCadastroInput.Senha
            };
            _usuarioRepository.Cadastrar(Usuario);
        }

        public void CadastrarUsuarioPadrao(UsuarioCadastroInput UsuarioCadastroInput)
        {
            var Usuario = new Usuario()
            {
                Nome = UsuarioCadastroInput.Nome,
                Email = UsuarioCadastroInput.Email,
                Tipo = TipoUsuario.Padrao,
                Senha = UsuarioCadastroInput.Senha
            };
            _usuarioRepository.Cadastrar(Usuario);
        }

        public void DeletarUsuario(int id)
        {
            _usuarioRepository.Deletar(id);
        }

        public IEnumerable<UsuarioDto> ObterTodosUsuariosDto()
        {
            var todosUsuarios = _usuarioRepository.ObterTodos();
            var usuariosDto = new List<UsuarioDto>();
            usuariosDto = todosUsuarios.Select(u => new UsuarioDto
            {
                Id = u.Id,
                DataCriacao = u.DataCriacao,
                Tipo = (TipoUsuarioDto)u.Tipo,
                Nome = u.Nome,
                Email = u.Email,
                Senha = u.Senha
            }
            ).ToList();

            return usuariosDto;
        }

        public UsuarioDto ObterUsuarioDtoPorId(int id)
        {
            var usuario = _usuarioRepository.ObterPorId(id);
            var usuarioDto = new UsuarioDto();

            usuarioDto.Id = usuario.Id;
            usuarioDto.DataCriacao = usuario.DataCriacao;
            usuarioDto.Tipo = (TipoUsuarioDto)usuario.Tipo;
            usuarioDto.Nome = usuario.Nome;
            usuarioDto.Email = usuario.Email;
            usuarioDto.Senha = usuario.Senha;

            return usuarioDto;
        }

        public UsuarioDto ObterUsuarioDtoPorNome(string nome)
        {
            var usuario = _usuarioRepository.obterPorNome(nome);
            var usuarioDto = new UsuarioDto();

            usuarioDto.Id = usuario.Id;
            usuarioDto.DataCriacao = usuario.DataCriacao;
            usuarioDto.Tipo = (TipoUsuarioDto)usuario.Tipo;
            usuarioDto.Nome = usuario.Nome;
            usuarioDto.Email = usuario.Email;
            usuarioDto.Senha = usuario.Senha;
            

            return usuarioDto;
        }
    }
}
