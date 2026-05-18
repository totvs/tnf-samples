# TNF Samples

Repositório de exemplos e cenários de uso do **TNF Framework** (TOTVS .NET Framework).

## Distribuição dos Pacotes

![Diagramas de pacotes do Tnf](https://github.com/totvs/tnf-samples/blob/master/Scenarios/tnf_packages_diagram.png)

## Estrutura do Repositório

```
tnf-samples/
├── Scenarios/          # Exemplos de backend com TNF Framework (.NET 8)
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

---

## Pacotes do TNF Framework

Catálogo completo de pacotes disponíveis no TNF, organizados por área funcional.

### Core

| Pacote | Descrição |
|--------|-----------|
| **Tnf.Kernel** | Pacote principal do framework com extensões de DI, Localization (arquivo) e Settings (arquivo) |
| **Tnf.Runtime** | Abstrações e implementações de Multi-Tenancy, segurança e sessão da aplicação |
| **Tnf.Notifications** | Abstrações e implementação do Notification Pattern |
| **Tnf.Dto** | DTOs para o padrão de API TOTVS (Request/Response) |
| **Tnf.Specifications** | Abstração e implementações do Specification Pattern |
| **Tnf.CloudEvents** | Suporte ao formato CloudEvents para mensagens e eventos |

### ASP.NET Core

| Pacote | Descrição |
|--------|-----------|
| **Tnf.AspNetCore** | Integração com ASP.NET Core (controllers, middleware, retorno de mensagem, sessão, notificações) |
| **Tnf.AspNetCore.Security** | Segurança e autenticação com suporte ao RAC (TOTVS Access Control) |
| **Tnf.AspNetCore.Security.FluigIdentity** | Autenticação via Fluig Identity (OAuth 2.0 / JWT / SSO) |
| **Tnf.AspNetCore.Metrics.Abstractions** | Abstrações para métricas e observabilidade |
| **Tnf.AspNetCore.Metrics** | Implementação de métricas com AppMetrics (Prometheus/Grafana) |

### Domain-Driven Design

| Pacote | Descrição |
|--------|-----------|
| **Tnf.Domain** | Infraestrutura para DDD: DomainServices, ApplicationService e Builder de entidades |
| **Tnf.Domain.Events** | Implementação do padrão Domain Events |
| **Tnf.Repositories** | Abstrações para repositórios, entidades, multi-organização, multi-tenancy e UnitOfWork |

### CQRS / Commands

| Pacote | Descrição |
|--------|-----------|
| **Tnf.Commands** | Implementação do padrão Command/Handler (CQRS) |
| **Tnf.Commands.FluentValidation** | Validação de commands com FluentValidation |

### Persistência / Data Access

| Pacote | Descrição |
|--------|-----------|
| **Tnf.EntityFrameworkCore** | Suporte ao EF Core com gerência de transações (UoW), filtros automáticos de multi-tenancy, soft delete e auditoria |
| **Tnf.Dapper** | Suporte ao Dapper baseado nas abstrações de Tnf.Repositories (reusa transações do EF Core) |
| **Tnf.MongoDb** | Implementação para MongoDB |
| **Tnf.Drivers.Devart** | Suporte a drivers Devart |
| **Tnf.Drivers.DevartOracle** | Suporte a Oracle via driver Devart DotConnect (licença de terceiro necessária) |

### Mensageria

| Pacote | Descrição |
|--------|-----------|
| **Tnf.Messaging** | Abstrações de mensageria |
| **Tnf.Messaging.RabbitMQ** | Implementação de mensageria com RabbitMQ |
| **Tnf.Bus.Queue** | Abstrações e implementações compartilhadas para filas |
| **Tnf.Bus.Queue.RabbitMQ** | Implementação de fila com RabbitMQ (abstração Bus) |
| **Tnf.Bus.Client** | Cliente de gerência de BUS |
| **Tnf.RabbitMq** | Infraestrutura base RabbitMQ |

### Jobs

| Pacote | Descrição |
|--------|-----------|
| **Tnf.Jobs** | Suporte a agendamento e execução de jobs |

### Cache

| Pacote | Descrição |
|--------|-----------|
| **Tnf.Caching.Abstractions** | Abstração de cache (inclui implementação de MemoryCache por padrão) |
| **Tnf.Caching.Redis** | Implementação de cache distribuído com Redis |

### Indexação / Busca

| Pacote | Descrição |
|--------|-----------|
| **Tnf.Indexing.Abstractions** | Abstrações para indexação e busca |
| **Tnf.Elasticsearch** | Implementação de indexação com Elasticsearch |

### Object Mapping

| Pacote | Descrição |
|--------|-----------|
| **Tnf.ObjectMapping.Abstractions** | Abstração de mapeamento de objetos |
| **Tnf.AutoMapper** | Implementação com AutoMapper |
| **Tnf.Mapster** | Implementação com Mapster |

### Localização e Configurações

| Pacote | Descrição |
|--------|-----------|
| **Tnf.Localization.Management** | Gerência de localização via banco de dados |
| **Tnf.Localization.EntityFrameworkCore** | Suporte a localização com EF Core |
| **Tnf.Settings.Management** | Gerência de configurações via banco de dados |
| **Tnf.Settings.Management.Redis** | Gerência de configurações com cache Redis |
| **Tnf.Settings.EntityFrameworkCore** | Suporte a configurações com EF Core |

### SmartX (Geração de Interfaces)

| Pacote | Descrição |
|--------|-----------|
| **Tnf.SmartX.Abstractions** | Abstrações do SmartX |
| **Tnf.SmartX** | Plataforma SmartX para geração automática de interfaces (Smart-UI / PO-UI) |
| **Tnf.SmartX.EntityFrameworkCore** | Integração SmartX com EF Core |
| **Tnf.SmartX.Security.FluigIdentity** | Segurança SmartX com Fluig Identity |

### SGDP (Privacidade de Dados / LGPD)

| Pacote | Descrição |
|--------|-----------|
| **Tnf.Sgdp.Abstractions** | Abstrações para conformidade com LGPD/SGDP |
| **Tnf.Sgdp** | Implementação do SGDP |
| **Tnf.Sgdp.EntityFrameworkCore** | Integração SGDP com EF Core |

### Integrações TOTVS

| Pacote | Descrição |
|--------|-----------|
| **Tnf.Carol** | Implementação de repositórios e conectividade com TOTVS Carol |
| **Tnf.Provider** | Abstração de HTTP Provider |
| **Tnf.Provider.Carol** | HTTP Provider para TOTVS Carol |
| **Tnf.LsCloud** | Integração com LS Cloud |
| **Tnf.LsCloud.Client** | Cliente para LS Cloud |

