# Scenarios – Exemplos TNF Framework

Todos os exemplos a seguir estão com Swagger configurado, log (Serilog) e compactação habilitados.

---

## Índice

| Projeto | Descrição | TNF Version |
|---------|-----------|-------------|
| [TNF-Zero](#tnf-zero) | Projeto mínimo de referência com DDD + CQRS | 8.x |
| [CarShopCrud](#carshopcrud) | CRUD completo com DDD, CQRS e mensageria | 8.x |
| [SmartX](#smartx) | Geração automática de interfaces com Smart-UI / PO-UI | 8.x |
| [JobSchedulerClient](#jobschedulerclient) | Cliente de agendamento de jobs | 8.x |

---

## [TNF-Zero](https://github.com/totvs/tnf-samples/tree/master/Scenarios/TNF-Zero)

Projeto mínimo de referência utilizando o **TNF Framework 8.x** sobre **.NET 8**, seguindo a arquitetura **Domain-Driven Design (DDD)** com separação em camadas e padrão CQRS. Serve como ponto de partida para novos microsserviços baseados no TNF.

- **Arquitetura:** DDD + CQRS + Clean Architecture
- **Banco de dados:** PostgreSQL + EF Core 8
- **Autenticação:** Fluig Identity (OAuth 2.0 / JWT)
- **Testes:** xUnit + Moq + FluentAssertions

- Pacotes TNF utilizados:
	- Tnf.AspNetCore
	- Tnf.AspNetCore.Security
	- Tnf.AspNetCore.Security.FluigIdentity
	- Tnf.Domain
	- Tnf.Repositories
	- Tnf.Commands
	- Tnf.Commands.FluentValidation
	- Tnf.EntityFrameworkCore

- Estrutura de projetos:
	- `TnfZero.API` – Web API (Controllers + Swagger + Serilog)
	- `TnfZero.Application` – Camada de aplicação (Commands / Handlers)
	- `TnfZero.Domain` – Entidades, interfaces de repositório e DTOs
	- `TnfZero.EntityFramework` – DbContext abstrato + Configurations + Repositories
	- `TnfZero.EntityFramework.PostgreSQL` – Provider concreto + DesignTimeFactory
	- `TnfZero.EntityFramework.Migrator` – Console App para executar migrations
	- `TnfZero.Tests` – Testes unitários

---

## [CarShopCrud](https://github.com/totvs/tnf-samples/tree/master/Scenarios/CarShopCrud)

Aplicação completa de gerenciamento de loja de carros com CRUD de Carros, Clientes, Concessionárias e Compras. Demonstra DDD com domínio rico, CQRS via `Tnf.Commands`, mensageria com RabbitMQ e enriquecimento de dados via tabela FIPE.

- **Arquitetura:** DDD (Domínio Rico) + CQRS + Mensageria
- **Banco de dados:** PostgreSQL + EF Core 8
- **Mensageria:** RabbitMQ (publicação de eventos e consumers)
- **Testes:** xUnit + Moq + FluentAssertions

- Pacotes TNF utilizados:
	- Tnf.AspNetCore
	- Tnf.AspNetCore.Security
	- Tnf.Commands
	- Tnf.Commands.FluentValidation
	- Tnf.EntityFrameworkCore
	- Tnf.Kernel
	- Tnf.Messaging
	- Tnf.Messaging.RabbitMQ
	- Tnf.Repositories

- Estrutura de projetos:
	- `Tnf.CarShop.Api` – Web API (Controllers + Swagger + Serilog)
	- `Tnf.CarShop.Application` – Commands, Handlers, Validators, Consumers e Localization
	- `Tnf.CarShop.Domain` – Entidades com domínio rico e interfaces de repositório
	- `Tnf.CarShop.EntityFramework` – DbContext abstrato + Configurations + Repositories
	- `Tnf.CarShop.EntityFramework.PostgreSql` – Provider concreto PostgreSQL
	- `Tnf.CarShop.EntityFramework.Migrator` – Console App para executar migrations

---

## [SmartX](https://github.com/totvs/tnf-samples/tree/master/Scenarios/SmartX)

Demonstra como usar o **TNF SmartX** para criar aplicações web dinâmicas com geração automática de interfaces baseadas em modelos de dados, utilizando componentes Smart-UI sobre o framework PO-UI da TOTVS. Suporta abordagens **Code-First** e **Database-First**.

- **Arquitetura:** SmartX + EF Core + PO-UI / Smart-UI
- **Banco de dados:** PostgreSQL (único suportado pelo SmartX)
- **Autenticação:** Fluig Identity
- **Abordagens:** Code-First (projetos novos) e Database-First (sistemas legados)

- Pacotes TNF utilizados:
	- Tnf.AspNetCore
	- Tnf.AspNetCore.Security.FluigIdentity
	- Tnf.SmartX
	- Tnf.SmartX.EntityFrameworkCore

- Estrutura de projetos:
	- `Tnf.SmartX` – Web API principal (Program.cs + Swagger + configuração SmartX)
	- `Tnf.SmartX.Domain` – Entidades Code-First e Database-First + Rotinas de interface
	- `Tnf.SmartX.EntityFrameworkCore` – Configurations de entidades
	- `Tnf.SmartX.EntityFramework.PostgreSql` – Provider concreto PostgreSQL
	- `Tnf.SmartX.EntityFramework.Migrator` – Console App para executar migrations

---

## [JobSchedulerClient](https://github.com/totvs/tnf-samples/tree/master/Scenarios/JobSchedulerClient)

Exemplo de como utilizar o pacote `Tnf.Jobs` para criar e gerenciar jobs agendados no ecossistema TNF. A aplicação se conecta ao Job Scheduler via RabbitMQ e expõe diversos exemplos de implementação de jobs com diferentes comportamentos.

- **Arquitetura:** Web API + Jobs agendados via RabbitMQ
- **Autenticação:** Fluig Identity (OAuth 2.0 / JWT)
- **Mensageria:** RabbitMQ (comunicação com Job Scheduler)

- Pacotes TNF utilizados:
	- Tnf.AspNetCore
	- Tnf.AspNetCore.Security
	- Tnf.AspNetCore.Security.FluigIdentity
	- Tnf.Jobs

- Estrutura de projetos:
	- `JobSchedulerClient.Web` – Web API com implementações de jobs de exemplo
