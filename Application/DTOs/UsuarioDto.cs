namespace Application.DTOs
{
    public enum TipoUsuarioDto
    {
        Padrao = 0,
        Administrador = 1
    }
    public class UsuarioDto
    {
        public int Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public string Nome { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Senha { get; set; } = null!;
        public TipoUsuarioDto Tipo { get; set; }
    }
}
