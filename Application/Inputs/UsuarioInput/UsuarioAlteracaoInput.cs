using System.ComponentModel.DataAnnotations;
using Domain.Entity;
using Application.Validations.DataAnnotations;

namespace Application.Input.UsuarioInput
{
    public class UsuarioAlteracaoInput
    {

        [Required(ErrorMessage = "Id é obrigatório.")]
        public required int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório.")]
        [MaxLength(20, ErrorMessage = "Nome pode ter no máximo 20 caracteres.")]
        public required string Nome { get; set; }

        [Required(ErrorMessage = "Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória.")]
        [SenhaAttribute(TamanhoMinimo = 8)]
        public required string Senha { get; set; }

        [Required(ErrorMessage = "Tipo é obrigatório.")]
        public required TipoUsuario Tipo { get; set; }
    }
}
