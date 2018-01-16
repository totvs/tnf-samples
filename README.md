# Como estão divididos os pacotes

Alguns cenários de utilização estão disponíveis em nosso repositório.
Primeiro é necessário conhecer um pouco da divisão dos pacotes disponibilizados pelo Tnf

![Diagramas de pacotes do Tnf](tnf_packages_diagram.png)

###### Conteúdo dos pacotes

**Tnf.Kernel**: Pacote principal do framework contém as dependências mais primitivas:
	- Localization
	- Setting (apenas arquivo)
	- Dependency Injection (Extensões): Toda infra de injeção de dependência do Tnf é utilizando o **Microsoft.Extensions.DependencyInjection**.

**Tnf.Runtime**: abstrações para Multi-Tenancy, segurança e sessão da aplicação.

**Tnf.Notifications**: pattern de notificação.

**Tnf.Dto**: objetos de dto do framework utilizados para o padrão de API da TOTVS. (Compilado para ter compatibilidade com versões anteriores do framework: .NETStandard, .NETFramework 4.6, e .NETFramework 4.5).

**Tnf.Repositories:** abstrações para entidades, multi-organização, multi-tenancy e pattern de UnitOfWork.

**Tnf.AspNetCore:** prove suporte ao AspNetCore com a infra-estrutura do Tnf para retorno de mensagem, sessão, tratamento de notificações geradas através do notification pattern.

**Tnf.Repositories.AspNetCore:** infra-estrutura para utilizar o UnitOfWork (middleware) em cada request da aplicação AspNet.

**Tnf.EntityFrameworkCore:** suporte ao EntityFrameworkCore com implementações de gerencia de transações (Uow), filtros automaticos de multi-tenancy, softdelete e audit.

**Tnf.Dapper:** suporte ao Dapper. Para utilização deste pacote é preciso também o Tnf.EntityFrameworkCore. O framework realiza a gerencia das transações abertas de cada contexto reaproveitando elas na utilização do Dapper.

**Tnf.Localization.Manager:** gerencia de localização via banco de dados.

**Tnf.Localization.Manager.EntityFrameworkCore:** gerencia de localização com o suporte do EntityFrameworkCore.

**Tnf.Settings.Manager:** gerencia de configuração via banco de dados.

**Tnf.Settings.Manager.EntityFrameworkCore:** gerencia de configuração com o suporte do EntityFrameworkCore.

**Tnf.Settings.AspNetCore:** apis para gerencia da configurações via banco de dados.

**Tnf.Domain:** infra-estrutura para trabalhar com Domain Drive Design e alguns serviços como de DomainService genéricos.

**Tnf.Domain.Events:** implementação do padrão de domain events.

**Tnf.Caching.Abstractions:** abstração de cache do framework. Por default este pacote contém o TnfMemoryCache.

**Tnf.Caching.Redis:** implementação do suporte ao Redis como cache.

**Tnf.Caching.Redis.JsonSerializer:** implementação do suporte a serialização via JSON para o Redis.

**Tnf.ObjectMapping.Abstractions:** abstração utilização de mappers dentro do framework.

**Tnf.AutoMapper:** implementação de mapper utilizando o AutoMapper.

**Tnf.Bus.Queue:** abstração de fila.

**Tnf.Bus.Client:** abstração de gerencia de fila.

**Tnf.Bus.Queue.RabbitMQ:** implementação do suporte a fila com o RabbitMQ.
