﻿namespace Domain.Entity
{
    public enum TipoUsuario
    {
        Padrao = 0,
        Administrador = 1
    }

    public class Usuario : EntityBase
    {
        public string Nome { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Senha { get; set; } = null!;
        public TipoUsuario Tipo { get; set; }
    }
}
