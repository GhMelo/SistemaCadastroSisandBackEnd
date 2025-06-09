﻿using System.ComponentModel.DataAnnotations;
using Application.Validations.DataAnnotations;

namespace Application.Input.UsuarioInput
{
    public class UsuarioCadastroInput
    {
        [Required(ErrorMessage = "Nome é obrigatório.")]
        public required string Nome { get; set; }

        [Required(ErrorMessage = "Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória.")]
        [SenhaAttribute(TamanhoMinimo = 8)]
        public required string Senha { get; set; }

    }
}
