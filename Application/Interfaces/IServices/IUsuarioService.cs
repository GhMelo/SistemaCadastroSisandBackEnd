using Application.DTOs;
using Application.Input.UsuarioInput;

namespace Application.Interfaces.IService
{
    public interface IUsuarioService
    {
        UsuarioDto ObterUsuarioDtoPorNome(string nome);
        UsuarioDto ObterUsuarioDtoPorId(int id);
        IEnumerable<UsuarioDto> ObterTodosUsuariosDto();
        void CadastrarUsuarioPadrao(UsuarioCadastroInput UsuarioCadastroInput);
        void CadastrarUsuarioAdministrador(UsuarioCadastroInput UsuarioCadastroInput);
        void AlterarUsuario(UsuarioAlteracaoInput UsuarioAlteracaoInput);
        void DeletarUsuario(int id);
    }
}
