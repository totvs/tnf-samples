# Job Scheduler Client

## Descrição

O projeto JobSchedulerClient é uma aplicação de exemplo que demonstra como utilizar o pacote `Tnf.Jobs` para criar e gerenciar jobs agendados no ecossistema TNF. A aplicação se conecta ao Job Scheduler via RabbitMQ e expõe diversos exemplos de implementação de jobs com diferentes comportamentos.

## Funcionalidades

### Jobs de Exemplo

- **FailingJob** – Demonstra o comportamento quando um job lança uma exceção durante a execução (marcado como falha).
- **MultipleStepsJob** – Demonstra como reportar progresso em etapas (frações) durante a execução de um job.
- **UnevenProgressJob** – Demonstra que o progresso de um job pode ser errático, com mudanças de fracionamento.
- **LogExceptionJob** – Demonstra como logar exceções sem causar a falha do job.
- **JobWithUserFriendlyName** – Demonstra o uso do atributo `[TnfJob]` para definir um identificador e descrição amigável.
- **LogSessionJob** – Demonstra o uso de injeção de dependência (`ITnfSession`) dentro de um job para acessar informações de sessão.
- **AllTypeOfParametersJob** – Demonstra todos os tipos de parâmetros aceitos por um job.
- **JobParameterAttributeJob** – Demonstra o uso do atributo `[TnfJobParameter]` para configurar labels e obrigatoriedade de parâmetros.

### Conceitos Demonstrados

- Autorização de jobs com `[TnfJobAuthorize]` (integração com RAC)
- Reporte de progresso via `SetProgressAsync`
- Log de exceções via `LogExceptionAsync`
- Log de texto via `LogTextAsync`
- Parâmetros tipados com classes de transporte
- Injeção de dependência em classes de job
- Configuração de nomes amigáveis com `[TnfJob]`

## Pré-requisitos

- .NET 8.0 SDK
- RabbitMQ (para comunicação com o Job Scheduler)
- Acesso ao feed NuGet privado da TOTVS (configurado em `nuget.config`)

## Configuração

A aplicação utiliza as seguintes seções de configuração no `appsettings`:

| Seção | Descrição |
|-------|-----------|
| `FluigIdentity` | Configuração de autenticação via Fluig Identity / RAC |
| `JobScheduler` | Conexão com o RabbitMQ (host, virtual host, credenciais) |
| `Serilog` | Configuração de logging estruturado |

### Exemplo de configuração (Development)

```json
{
  "FluigIdentity": {
    "AuthorityEndpoint": "https://admin.rac.dev.totvs.app/totvs.rac",
    "ClientId": "JobScheduleClientSample",
    "ClientSecret": "<seu-secret>"
  },
  "JobScheduler": {
    "HostName": "localhost",
    "VirtualHost": "support-element-job-scheduler",
    "UserName": "guest",
    "Password": "guest"
  }
}
```

## Instalação

1. **Clone o repositório:**

   ```bash
   git clone [url-do-repositório]
   ```

2. **Navegue até a pasta do projeto:**

   ```bash
   cd Scenarios/JobSchedulerClient
   ```

3. **Restaure as dependências:**

   ```bash
   dotnet restore
   ```

4. **Execute a aplicação:**

   ```bash
   dotnet run --project src/JobSchedulerClient.Web
   ```

A API estará disponível com Swagger em `https://localhost:<porta>/swagger` no ambiente de desenvolvimento.

## Estrutura do Projeto

```
JobSchedulerClient/
├── Directory.Packages.props        # Gerenciamento central de versões (CPM)
├── nuget.config                    # Feed NuGet privado TOTVS
├── global.json                     # SDK .NET 8.0
├── JobSchedulerClient.sln
└── src/
    └── JobSchedulerClient.Web/
        ├── Program.cs              # Configuração da aplicação (TNF, Security, Jobs, Swagger)
        ├── Jobs.cs                 # Implementações dos jobs de exemplo
        ├── Controllers/
        │   └── ValuesController.cs # Controller de exemplo
        └── appsettings.*.json      # Configurações por ambiente
```

## Dependências

- Tnf.AspNetCore 8.x
- Tnf.AspNetCore.Security 8.x
- Tnf.AspNetCore.Security.FluigIdentity 8.x
- Tnf.Jobs 8.x
- Swashbuckle.AspNetCore 6.6.2
- Serilog.AspNetCore 8.0.3
