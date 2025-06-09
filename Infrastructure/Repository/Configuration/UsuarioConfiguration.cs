using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Repository.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnType("INT");
            builder.Property(p => p.DataCriacao).HasColumnName("DataCriacao").HasColumnType("DATETIME").IsRequired();
            builder.Property(p => p.Nome).HasColumnName("Nome").HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(p => p.Email).HasColumnName("Email").HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(p => p.Senha).HasColumnName("Senha").HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(p => p.Tipo).HasColumnName("Tipo").HasColumnType("INT").IsRequired();
        }
    }
}
