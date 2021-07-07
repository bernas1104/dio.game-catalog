# Digital Innovation One - Desenvolvimento de uma API Rest de catálogo de jogos utilizando o ASP.NET

Neste projeto foram abordados os seguintes tópicos:

- Criação de modelo de dados para mapeamento de entidades em banco de dados;
- Desenvolvimento de operações de gerenciamento do catálogo de jogos (inserção, leitura, atualização e remoção);
- Relação de cada uma das operações acima com o padrão arquitetural REST, e a explicação de cada um dos conceitos REST envolvidos durante o desenvolvimento do projeto;

## Build & Run
Para executar o projeto, é necessário um banco de dados PostgreSQL. É possível criar um por meio do [Docker](https://hub.docker.com/_/postgres). Para este projeto funcionar sem necessidade de alterações, execute:

```bash
docker run --name dio-postgress -e POSTGRES_PASSWORD=123456 -p 5432:5432 -d postgres
```

Após a configuração do docker, basta rodar os comandos:

```bash
dotnet tool install --global dotnet-ef

dotnet restore

dotnet ef database update

dotnet run
```

Caso esteja no Visual Studio Code, é possível substituir o comando "dotnet run" pela hotkey F5 ou CTRL + F5 (para debug).

## SDK & Libraries
O projeto foi criado utilizado a .NET SDK 5.0.201 e utiliza as seguintes libraries:
- AutoMapper
- AutoMapper.Extensions.Microsoft.DependencyInjection
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Design
- Npgsql.EntityFrameworkCore.PostgreSQL
- Swashbuckle.AspNetCore
