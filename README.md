# TNF Samples

Repositório de exemplos e cenários de uso do **TNF Framework** (TOTVS .NET Framework).

## Distribuição dos Pacotes

![Diagramas de pacotes do Tnf](https://github.com/totvs/tnf-samples/blob/master/Scenarios/tnf_packages_diagram.png)

## Estrutura do Repositório

```
tnf-samples/
├── Scenarios/          # Exemplos de backend com TNF Framework (.NET)
│   ├── TNF-Zero/       # Projeto mínimo de referência (DDD + CQRS)
│   ├── CarShopCrud/    # CRUD completo com DDD, CQRS e mensageria
│   ├── SmartX/         # Geração automática de interfaces (Smart-UI / PO-UI)
│   └── JobSchedulerClient/  # Cliente de agendamento de jobs
│
└── Components/         # Exemplos de frontend
    └── eula-modal/     # Componente Angular de modal EULA
```

## Scenarios

Exemplos de aplicações backend utilizando o TNF Framework. Todos os projetos incluem Swagger, Serilog e compactação habilitados.

| Projeto | Descrição | TNF Version |
|---------|-----------|-------------|
| [TNF-Zero](./Scenarios/TNF-Zero) | Projeto mínimo de referência com DDD + CQRS | 8.x |
| [CarShopCrud](./Scenarios/CarShopCrud) | CRUD completo com DDD, CQRS e mensageria RabbitMQ | 8.x |
| [SmartX](./Scenarios/SmartX) | Geração automática de interfaces com Smart-UI / PO-UI | 8.x |
| [JobSchedulerClient](./Scenarios/JobSchedulerClient) | Cliente de agendamento de jobs com Tnf.Jobs | 8.x |

Para mais detalhes sobre cada cenário, consulte o [README dos Scenarios](./Scenarios/README.md).

## Descrição dos Pacotes TNF

**Tnf.Kernel**: Pacote principal do framework contém as dependências mais primitivas (Localization, Settings, DI Extensions).

**Tnf.AspNetCore**: Suporte ao ASP.NET Core com infraestrutura do TNF para retorno de mensagem, sessão e tratamento de notificações.

**Tnf.Commands**: Implementação do padrão CQRS com CommandHandlers.

**Tnf.Commands.FluentValidation**: Validação de commands com FluentValidation.

**Tnf.Domain**: Infraestrutura para Domain-Driven Design e serviços de domínio.

**Tnf.Repositories**: Abstrações para entidades, multi-organização, multi-tenancy e UnitOfWork.

**Tnf.EntityFrameworkCore**: Suporte ao EF Core com gerência de transações, filtros automáticos de multi-tenancy, soft delete e auditoria.

**Tnf.Messaging**: Abstração de mensageria.

**Tnf.Messaging.RabbitMQ**: Implementação de mensageria com RabbitMQ.

**Tnf.Jobs**: Suporte a agendamento e execução de jobs.

**Tnf.SmartX**: Geração automática de interfaces baseadas em modelos de dados.

**Tnf.AspNetCore.Security**: Middleware de segurança e autenticação.

**Tnf.AspNetCore.Security.FluigIdentity**: Integração com Fluig Identity (OAuth 2.0 / JWT).
