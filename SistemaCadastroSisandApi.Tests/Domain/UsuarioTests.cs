using Bogus;
using Domain.Entity;
using NUnit.Framework;

namespace SistemaCadastroSisandApi.Tests.Domain;

[TestFixture]
public class UsuarioTests
{
    private readonly Faker _faker = new();
    [Test]
    public void CriarUsuario_DeveDefinirPropriedadesCorretamente()
    {
        var id = _faker.Random.Int(1, 10000);
        var nome = _faker.Name.FullName();
        var email = _faker.Internet.Email();
        var senha = _faker.Internet.Password(12, true, prefix: "Senha");
        var tipo = _faker.PickRandom<TipoUsuario>();

        var usuario = new Usuario
        {
            Id = id,
            Nome = nome,
            Email = email,
            Senha = senha,
            Tipo = tipo
        };

        Assert.That(usuario.Id, Is.EqualTo(id));
        Assert.That(usuario.Nome, Is.EqualTo(nome));
        Assert.That(usuario.Email, Is.EqualTo(email));
        Assert.That(usuario.Senha, Is.EqualTo(senha));
        Assert.That(usuario.Tipo, Is.EqualTo(tipo));

        Assert.That(usuario.DataCriacao, Is.EqualTo(DateTime.Now).Within(TimeSpan.FromSeconds(2)));
    }
    [Test]
    public void CriarUsuario_NaoDeveDefinirPropriedadesObrigatorias_QuandoNaoPreenchidas()
    {
        var usuario = new Usuario();

        Assert.That(usuario.Nome, Is.Null.Or.Empty);
        Assert.That(usuario.Email, Is.Null.Or.Empty);
        Assert.That(usuario.Senha, Is.Null.Or.Empty);
        Assert.That(usuario.Tipo, Is.EqualTo(default(TipoUsuario)));
        Assert.That(usuario.DataCriacao, Is.Not.Null);
    }

}