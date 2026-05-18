# TnfZero

Projeto mínimo de referência utilizando o **TNF Framework 8.x** sobre **.NET 8**, seguindo a arquitetura **Domain-Driven Design (DDD)** com separação em camadas e padrão CQRS.

Este repositório serve como ponto de partida para novos microsserviços baseados no TNF dentro do ecossistema TOTVS.

---

## 📋 Estrutura do Projeto

```
TnfZero/
├── src/
│   ├── TnfZero.Domain/                          # Camada de Domínio
│   │   ├── Entities/DogEntity.cs                 #   Entidade raiz do agregado
│   │   ├── Repositories/IDogRepository.cs        #   Contrato do repositório (interface)
│   │   └── Dtos/DogDto.cs                        #   DTOs de leitura e paginação
│   │
│   ├── TnfZero.Application/                     # Camada de Aplicação (CQRS)
│   │   └── Commands/
│   │       ├── CreateDog/                        #   CreateDogCommand + Handler
│   │       ├── UpdateDog/                        #   UpdateDogCommand + Handler
│   │       └── DeleteDog/                        #   DeleteDogCommand + Handler
│   │
│   ├── TnfZero.EntityFramework/                  # Infraestrutura – DbContext abstrato
│   │   ├── TnfZeroDbContext.cs                   #   Herda de TnfDbContext (multi-tenant, auditoria)
│   │   ├── Configurations/                       #   IEntityTypeConfiguration<T> por entidade
│   │   │   └── DogEntityConfiguration.cs
│   │   └── Repositories/                         #   Implementações EF Core dos repositórios
│   │       └── DogRepository.cs
│   │
│   ├── TnfZero.EntityFramework.PostgreSQL/       # Infraestrutura – Provider concreto
│   │   ├── PostgreSqlTnfZeroDbContext.cs         #   DbContext concreto (herda do abstrato)
│   │   ├── PostgreSqlTnfZeroDbContextFactory.cs  #   IDesignTimeDbContextFactory (EF CLI)
│   │   └── appsettings.Development.json          #   Connection string para tooling
│   │
│   ├── TnfZero.EntityFramework.Migrator/         # Console App – Executor de Migrations
│   │   ├── Program.cs                            #   Host mínimo + MigrateAsync()
│   │   ├── appsettings.json                      #   Config de produção
│   │   └── appsettings.Development.json          #   Config de desenvolvimento
│   │
│   └── TnfZero.API/                             # Camada de Apresentação (Web API)
│       ├── Program.cs                            #   Bootstrap: TNF + Serilog + Swagger + CQRS
│       ├── Controllers/DogsController.cs         #   REST controller com CRUD completo
│       ├── appsettings.json                      #   Configuração geral
│       └── appsettings.Development.json          #   Configuração de desenvolvimento
│
├── tests/
│   └── TnfZero.Tests/                           # Testes unitários
│       ├── Domain/DogEntityTests.cs              #   Testes da entidade
│       └── Application/CreateDogCommandHandlerTests.cs  # Testes do handler
│
├── devops/pipelines/                             # CI/CD Azure DevOps (TOTVS template)
├── nuget.config                                  # Feed privado TOTVS + packageSourceMapping
├── Directory.Packages.props                      # Central Package Management (CPM)
└── TnfZero.sln
```

---

## 🏗️ Arquitetura e Camadas

### Domain (`TnfZero.Domain`)

Camada mais interna — sem dependências externas além das interfaces do TNF.

- **Entidades** implementam interfaces do TNF como `IHasCreationTime`, `IHasModificationTime` e `IMustHaveTenant` para auditoria e multi-tenancy automáticos.
- **Repositórios** são definidos como interfaces (`IDogRepository`) que estendem `IRepository<T>` do TNF.
- **DTOs** usam `RequestAllDto` do TNF para paginação padronizada (Page, PageSize, Order, Fields).

### Application (`TnfZero.Application`)

Implementa o padrão **CQRS** usando `Tnf.Commands`:

- Cada comando é um `record` imutável (ex: `CreateDogCommand(string Name)`).
- Cada handler herda de `CommandHandler<TCommand, TResult>` do TNF.
- Organização em **Vertical Slices** — cada comando em sua própria pasta.

### EntityFramework (`TnfZero.EntityFramework`)

DbContext **abstrato** que herda de `TnfDbContext` — a classe base do TNF que aplica automaticamente:

- Filtro global de **multi-tenancy** (`TenantId`)
- Preenchimento automático de campos de **auditoria** (`CreationTime`, `LastModificationTime`)
- Integração com `ITnfSession` para contexto do usuário/tenant

As configurações de entidade ficam em `Configurations/` usando `IEntityTypeConfiguration<T>` e são registradas via `modelBuilder.ApplyConfiguration(...)`.

### EntityFramework.PostgreSQL (`TnfZero.EntityFramework.PostgreSQL`)

Provider **concreto** — separa a escolha do banco de dados da lógica de mapeamento:

- `PostgreSqlTnfZeroDbContext` — herda do DbContext abstrato, sem lógica adicional.
- `PostgreSqlTnfZeroDbContextFactory` — implementa `IDesignTimeDbContextFactory` para que o CLI do EF Core (`dotnet ef`) consiga criar o contexto em design-time.

> **Por que essa separação?** Permite trocar o provider (ex: SQL Server, SQLite para testes) sem alterar mapeamentos ou repositórios.

### EntityFramework.Migrator (`TnfZero.EntityFramework.Migrator`)

Console App dedicado exclusivamente a **aplicar migrations** no banco de dados.

---

## 🔄 Como Funciona o Migrator

O Migrator é um **Console App (.NET 8)** independente que executa `Database.MigrateAsync()` contra o banco configurado. Ele existe separado da API para permitir:

1. **Deploy independente** — migrations rodam antes da API subir (ideal para pipelines CI/CD e Kubernetes init containers).
2. **Isolamento de responsabilidade** — a API nunca aplica migrations em runtime.
3. **Execução em qualquer ambiente** — basta trocar a connection string via `appsettings.json` ou variáveis de ambiente.

### Fluxo de execução

```
┌─────────────────────────────────────────────────────┐
│  TnfZero.EntityFramework.Migrator / Program.cs      │
├─────────────────────────────────────────────────────┤
│  1. Host.CreateDefaultBuilder(args)                 │
│     → Carrega appsettings.json + Environment        │
│                                                     │
│  2. ConfigureServices                               │
│     → Lê ConnectionStrings:Default                  │
│     → Registra AddTnfDbContext<Abstract, Concrete>  │
│       com UseNpgsql(connectionString)               │
│                                                     │
│  3. Cria um scope de DI                             │
│     → Resolve TnfZeroDbContext                      │
│                                                     │
│  4. await db.Database.MigrateAsync()                │
│     → Aplica todas as migrations pendentes          │
│     → Cria o banco se não existir (EnsureCreated)   │
└─────────────────────────────────────────────────────┘
```

### Código do Migrator

```csharp
var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var connectionString = context.Configuration.GetConnectionString("Default")
            ?? throw new InvalidOperationException(
                "Connection string 'Default' not found in configuration.");

        services.AddTnfDbContext<TnfZeroDbContext, PostgreSqlTnfZeroDbContext>(c =>
        {
            c.DbContextOptions.UseNpgsql(connectionString);
        });
    })
    .Build();

await using var scope = host.Services.CreateAsyncScope();
var db = scope.ServiceProvider.GetRequiredService<TnfZeroDbContext>();

Console.WriteLine("Applying EF Core migrations...");
await db.Database.MigrateAsync();
Console.WriteLine("Migrations applied successfully.");
```

### Quando usar

| Cenário | Comando |
|---------|---------|
| Desenvolvimento local | `cd src/TnfZero.EntityFramework.Migrator && dotnet run` |
| Pipeline CI/CD | Etapa antes do deploy da API |
| Kubernetes | Init container que roda o Migrator antes do pod da API |
| Docker Compose | `depends_on` com o Migrator rodando primeiro |

### Criando novas migrations

As migrations são geradas a partir do projeto **PostgreSQL** (onde está o `IDesignTimeDbContextFactory`):

```bash
cd src/TnfZero.EntityFramework.PostgreSQL
dotnet ef migrations add NomeDaMigration
```

Depois, execute o Migrator para aplicá-las:

```bash
cd src/TnfZero.EntityFramework.Migrator
dotnet run
```

---

## 🎯 Funcionalidades

| Recurso | Tecnologia |
|---------|-----------|
| Framework | TNF 8.x (.NET 8) |
| Banco de Dados | PostgreSQL + EF Core 8 |
| Arquitetura | DDD + CQRS + Clean Architecture |
| API | ASP.NET Core com Controllers |
| Documentação API | Swagger / OpenAPI (Swashbuckle) |
| Logging | Serilog (console + arquivo rotativo em `logs/`) |
| Multi-tenancy | Filtro global automático via `TnfDbContext` |
| Auditoria | `CreationTime` / `LastModificationTime` automáticos |
| Testes | xUnit + Moq + FluentAssertions |
| Pacotes | Central Package Management (CPM) |
| CI/CD | Azure DevOps pipelines (TOTVS template) |

---

## 🚀 Começando

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL 12+](https://www.postgresql.org/download/)
- IDE: Visual Studio 2022, JetBrains Rider ou VS Code

### 1. Configuração do Banco de Dados

Suba um PostgreSQL via Docker:

```bash
docker run -d \
  --name tnfzero-postgres \
  -e POSTGRES_USER=postgres \
  -e POSTGRES_PASSWORD=admin \
  -e POSTGRES_DB=TnfZeroDb \
  -p 5432:5432 \
  postgres:16-alpine
```

> O container já cria o banco `TnfZeroDb` automaticamente com as credenciais configuradas nos `appsettings.Development.json`.

### 2. Executar Migrations (obrigatório na primeira vez)

Antes de rodar a API pela primeira vez, é necessário aplicar as migrations para criar as tabelas no banco:

```bash
cd src/TnfZero.EntityFramework.Migrator
dotnet run
```

> ⚠️ **Sem este passo a API não funcionará**, pois as tabelas ainda não existem no banco. O Migrator executa `MigrateAsync()` que cria toda a estrutura necessária.

### 3. Executar a API

```bash
cd src/TnfZero.API
dotnet run
```

Acesse o Swagger UI em: `http://localhost:5000/swagger`

### 4. Executar os Testes

```bash
dotnet test
```

---

## 📚 Endpoints da API

| Método | Rota | Descrição |
|--------|------|-----------|
| `GET` | `/api/Dogs` | Listar todos (paginado) |
| `GET` | `/api/Dogs/{id}` | Buscar por ID |
| `POST` | `/api/Dogs` | Criar novo |
| `PUT` | `/api/Dogs/{id}` | Atualizar existente |
| `DELETE` | `/api/Dogs/{id}` | Excluir |

---

## � Desenvolvimento

### Adicionando uma nova entidade

1. Crie a entidade em `Domain/Entities/`
2. Crie a interface do repositório em `Domain/Repositories/`
3. Crie a configuração EF em `EntityFramework/Configurations/`
4. Registre no `OnModelCreating` do `TnfZeroDbContext`
5. Crie a implementação do repositório em `EntityFramework/Repositories/`
6. Crie os comandos CQRS em `Application/Commands/`
7. Crie o controller em `API/Controllers/`
8. Gere a migration: `dotnet ef migrations add NovaMigration`

### Adicionando uma nova migration

```bash
cd src/TnfZero.EntityFramework.PostgreSQL
dotnet ef migrations add NomeDaMigration
dotnet ef database update   # ou rode o Migrator
```

---

## 📦 Gerenciamento de Pacotes

Esta solução utiliza **Central Package Management (CPM)**:

- Todas as versões ficam centralizadas em `Directory.Packages.props`
- Arquivos `.csproj` referenciam pacotes **sem** atributo `Version`
- Pacotes TNF usam versão flutuante `8.*` para receber patches automaticamente
- O `nuget.config` configura o feed privado TOTVS com `packageSourceMapping` para segurança

---

## 🔐 Configuração

### Connection Strings

Edite `appsettings.Development.json` no projeto desejado:

```json
{
  "ConnectionStrings": {
    "Default": "Host=localhost;Port=5432;Database=TnfZeroDb;User Id=postgres;Password=admin"
  }
}
```

### Serilog

Configurado em `appsettings.json` da API:

```json
{
  "Serilog": {
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft.AspNetCore": "Warning",
        "Microsoft.EntityFrameworkCore": "Warning"
      }
    }
  }
}
```

Logs são escritos no console e em arquivos rotativos em `logs/tnfzero-YYYYMMDD.log`.

### Fluig Identity (Autenticação OAuth 2.0 / SSO)

**Fluig Identity** é o provedor de identidade SSO da TOTVS, baseado no protocolo **OAuth 2.0 / OpenID Connect**. Ele centraliza a autenticação de todos os serviços do ecossistema TOTVS, emitindo tokens JWT que são validados via JWKS.

#### O que ele faz neste projeto

| Componente | Função |
|------------|--------|
| `Tnf.AspNetCore.Security` | Middleware `UseTnfAspNetCoreSecurity()` que intercepta requisições e valida o JWT |
| `Tnf.AspNetCore.Security.FluigIdentity` | Configura `AddFluigIdentitySecurity(configuration)` com os endpoints e credenciais |
| `[TnfAuthorize]` | Atributo nos controllers que exige token válido na requisição (aplicado em `DogsController`) |

#### Onde ficam as configurações

As configurações ficam em **`src/TnfZero.API/appsettings.json`**, na seção `FluigIdentity`:

```json
{
  "FluigIdentity": {
    "FluigIdentityAddress": "https://app.fluigidentity.net",
    "MaestroAddress":       "https://api-fluig.dev.totvs.app",
    "TokenEndpoint":        "https://app.fluigidentity.net/accounts/oauth/token",
    "JwksEndpoint":         "https://app.fluigidentity.net/accounts/api/v1/jwks",
    "AuthorityEndpoint":    "https://admin.rac.dev.totvs.app/totvs.rac",
    "ClientId":             "<seu-client-id>",
    "ClientSecret":         "<seu-client-secret>"
  }
}
```

> ⚠️ **Nunca versione `ClientSecret` com valores reais.** Em produção, injete via variável de ambiente (`FluigIdentity__ClientSecret=...`) ou secret manager.

| Chave | Descrição |
|-------|-----------|
| `FluigIdentityAddress` | URL base do Fluig Identity |
| `MaestroAddress` | URL da API Maestro (resolução de tenants) |
| `TokenEndpoint` | Endpoint OAuth 2.0 para obtenção de tokens |
| `JwksEndpoint` | Endpoint JWKS para validação da assinatura do JWT |
| `AuthorityEndpoint` | Issuer autorizado para validação do token |
| `ClientId` | Identificador do cliente OAuth registrado no Fluig Identity |
| `ClientSecret` | Segredo do cliente (manter fora do controle de versão) |

#### Como ativar no `Program.cs`

O scaffolding inclui os pacotes mas a segurança precisa ser ligada manualmente. Adicione ao `Program.cs`:

```csharp
// ── Segurança – Fluig Identity ─────────────────────────────────────────────
builder.Services.AddFluigIdentitySecurity(builder.Configuration);

// ... depois de builder.Build():
app.UseTnfAspNetCoreSecurity();
```

Os pacotes já estão declarados em `Directory.Packages.props` — basta referenciá-los no `.csproj` da API:

```xml
<PackageReference Include="Tnf.AspNetCore.Security" />
<PackageReference Include="Tnf.AspNetCore.Security.FluigIdentity" />
```
