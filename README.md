# Sistema de Cadastro de Usuarios Sisand

API REST desenvolvida para o projeto da Sisand, com foco em gerenciamento de usuários. O projeto segue princípios de DDD, utiliza autenticação JWT e trabalha com persistência em SQL Server e MongoDB.

## Tecnologias

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- MongoDB
- JWT (Json Web Token)
- Swagger
- NUnit, Moq, Bogus (para testes)

## Instalação

1. Clone o repositório

2. Configure a connection string no appsettings.json
Tenha uma instância de SQL Server e MongoDB. Existem duas connectionStrings neste arquivo, um do MongoDb que está dentro do MongoSettings, essa é a linha para a conexão com o servidor de logs, e o outro ConnectionString que está dentro de ConnectionStrings, que é a linha de conexão com o banco SQL Server.

3. Restaure os pacotes e Rode as Migrations
Se estiver utilizando o Visual Studio, basta clicar como botão direito na solução e clicar em restore nuget packages para restaurar os pacotes da solução, se precisar por linha de código, é o comando dotnet restore no powershell de desenvolvedor

cd .\SistemaCadastroSisand\
cd ‘.\SistemaCadastroSisand\’
dotnet restore

Abra o console do gerenciador de pacotes do visual studio(Ou IDE equivalente), acessando o projeto de INFRASTRUCTURE e escreva 
Update-Database -Connection “String de conexao com o banco SQL Server”

4. Usuario administrador 

Após executar as migrations, é necessário inserir manualmente um usuário com perfil de administrador no banco de dados, pois apenas administradores podem cadastrar novos administradores, pelo frontend não deixei nenhuma função de cadastro de usuarios administrador, porem no backend tem a rota referente para testar pelo swagger.

INSERT INTO SistemaCadastroSisand.dbo.Usuario (Nome, Email, Senha, Tipo, DataCriacao)
VALUES ('Admin','admin@sisand.com', 'SenhaForte123.',1, GETDATE());

5. Executando a aplicação
Se estiver no visual studio é só executar pela propria IDE, se não, use o dotnet run pelo powershell de desenvolvedor, na pasta da API principal. O swagger estará disponível no localhost.

Powershell desenvolvedor: 

Navegue ate SistemaCadastroSisand

dotnet dev-certs https –trust

dotnet run –launch-profile “https”

https://localhost:7020/swagger/index.html

6. Autenticação
A API usa autenticação JWT. Para acessar os endpoints protegidos:

Realize o login para obter um token.

Envie o token no cabeçalho Authorization:
Authorization: Bearer {token}

7. Executando os testes
Para executar os testes no visual studio é só clicar com o botão direito no projeto de testes e clicar em “Executar testes” ou por linha de código, navegue até a raiz do projeto e execute o comando dotnet test no powershell de desenvolvedor


