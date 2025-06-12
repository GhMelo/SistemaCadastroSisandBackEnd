using Bogus;
using Domain.Entity;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace SistemaCadastroSisandApi.Tests.Infrastructure;

[TestFixture]
public class UsuarioRepositoryTests
{
    private ApplicationDbContext _context;
    private UsuarioRepository _repository;
    private readonly Faker _faker = new();
    private Usuario GerarUsuarioFaker()
    {
        return new Usuario
        {
            Id = _faker.Random.Int(1, 10000),
            Nome = _faker.Name.FullName(),
            Email = _faker.Internet.Email(),
            Senha = _faker.Internet.Password(12, true, prefix: "Senha"),
            Tipo = _faker.PickRandom<TipoUsuario>()
        };
    }

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        _context = new ApplicationDbContext(options);
        //Deleta o banco de dados toda vez para um teste não interferir no outro
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();

        _repository = new UsuarioRepository(_context);
    }

    [Test]
    public void ObterPorNome_DeveRetornarUsuario_QuandoNomeExiste()
    {
        var usuario = GerarUsuarioFaker();
        _repository.Cadastrar(usuario);

        var resultado = _repository.obterPorNome(usuario.Nome);

        Assert.That(resultado, Is.Not.Null);
        Assert.That(resultado!.Nome, Is.EqualTo(usuario.Nome));
    }
    [Test]
    public void ObterPorNome_DeveRetornarNull_QuandoNomeNaoExiste()
    {
        var resultado = _repository.obterPorNome("Inexistente");
        Assert.That(resultado, Is.Null);
    }
    [Test]
    public void ObterPorId_DeveRetornarUsuarioComIdInformado()
    {
        var usuario = GerarUsuarioFaker();
        _repository.Cadastrar(usuario);

        var resultado = _repository.ObterPorId(usuario.Id);

        Assert.That(resultado, Is.Not.Null);
        Assert.That(resultado!.Id, Is.EqualTo(usuario.Id));
    }
    [Test]
    public void ObterTodos_DeveRetornarTodosUsuariosDoBanco()
    {
        var usuario1 = GerarUsuarioFaker();
        var usuario2 = GerarUsuarioFaker();

        _repository.Cadastrar(usuario1);
        _repository.Cadastrar(usuario2);

        var resultado = _repository.ObterTodos();

        Assert.That(resultado.Count, Is.EqualTo(2));
        Assert.That(resultado.Any(u => u.Id == usuario1.Id), Is.True);
        Assert.That(resultado.Any(u => u.Id == usuario2.Id), Is.True);
    }
    [Test]
    public void ObterPorId_DeveRetornarNull_QuandoIdNaoExiste()
    {
        var resultado = _repository.ObterPorId(999999);
        Assert.That(resultado, Is.Null);
    }
    [Test]
    public void Cadastrar_DeveAdicionarUsuarioAoBanco()
    {
        var usuario = GerarUsuarioFaker();

        _repository.Cadastrar(usuario);

        var usuarioNoBanco = _repository.ObterPorId(usuario.Id);

        Assert.That(usuarioNoBanco, Is.Not.Null);
        Assert.That(usuarioNoBanco!.Nome, Is.EqualTo(usuario.Nome));
        Assert.That(usuarioNoBanco.DataCriacao, Is.Not.EqualTo(default(DateTime)));
    }
    [Test]
    public void CadastrarUsuario_DeveLancarExcecao_QuandoCamposObrigatoriosNaoForemPreenchidos()
    {
        var usuarioInvalido = new Usuario();

        //Usuario Inteiro vazio
        Assert.Throws<Exception>(() =>
        {
            _repository.Cadastrar(usuarioInvalido);
        });

        //Somente nome preenchido
        Assert.Throws<Exception>(() =>
        {
            usuarioInvalido.Email = null;
            usuarioInvalido.Senha = null;
            usuarioInvalido.Nome = _faker.Name.FullName();
            _repository.Cadastrar(usuarioInvalido);
        });

        //Somente Email preenchido
        Assert.Throws<Exception>(() =>
        {
            usuarioInvalido.Senha = null;
            usuarioInvalido.Nome = null;
            usuarioInvalido.Email = _faker.Internet.Email();
            _repository.Cadastrar(usuarioInvalido);
        });

        //Somente senha preenchida
        Assert.Throws<Exception>(() =>
        {
            usuarioInvalido.Email = null;
            usuarioInvalido.Nome = null;
            usuarioInvalido.Senha = _faker.Internet.Password(12, true, prefix: "Senha");
            _repository.Cadastrar(usuarioInvalido);
        });
    }
    [Test]
    public void Alterar_DeveAtualizarDadosDoUsuario()
    {
        var usuario = GerarUsuarioFaker();
        _repository.Cadastrar(usuario);

        var novoEmail = _faker.Internet.Email();
        usuario.Email = novoEmail;
        _repository.Alterar(usuario);

        var atualizado = _repository.ObterPorId(usuario.Id);

        Assert.That(atualizado, Is.Not.Null);
        Assert.That(atualizado!.Email, Is.EqualTo(novoEmail));
    }
    [Test]
    public void Alterar_DeveLancarExcecao_QuandoUsuarioNaoExiste()
    {
        var usuarioFake = GerarUsuarioFaker();
        Assert.Throws<Exception>(() => _repository.Alterar(usuarioFake));
    }
    [Test]
    public void AlterarUsuario_DeveLancarExcecao_QuandoIdForInvalido()
    {
        var usuario = GerarUsuarioFaker();
        _repository.Cadastrar(usuario);

        usuario.Id = 0;

        Assert.Throws<Exception>(() => _repository.Alterar(usuario));
    }
    [Test]
    public void Deletar_DeveRemoverUsuarioDoBanco()
    {
        var usuario = GerarUsuarioFaker();
        _repository.Cadastrar(usuario);

        _repository.Deletar(usuario.Id);

        var removido = _repository.ObterPorId(usuario.Id);

        Assert.That(removido, Is.Null);
    }
    [Test]
    public void Deletar_DeveLancarExcecao_QuandoIdNaoExiste()
    {
        Assert.Throws<Exception>(() => _repository.Deletar(999999999));
    }

}

