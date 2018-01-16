# TNF Samples (TnfSample-Architecture)

Exemplo da arquitetura e padrões de design do framework TNF.

- Camadas de API
- Camadas de Aplicação
- Camadas de Domínio
- Camadas de Repositório
- Migrations
- Configurações
- Builders
- Entidades de domínio
- Injeção de dependência
- Testes unitários
- Localização
- DTOs
- Mappers
- Validação de especificações
- Notificações de Domínio
- Mensageria com RabbitMQ
- Repositório com Carol

Este projeto reune exemplos de utilização do Framework cross-platform desenvolvido pela TOTVS em .NET Core, contendo:
1. N-Layered Architecture
2. Validações na camada de domínio: builders e specifications
3. Multiplos repositórios de dados: Carol, EF Core
4. Teste a nível de domain, applicatication e services, utilizando: xunit, NSubstitute, EF in memory e utilitários do frame TNF.
## Get started ##

Os passos abaixo guiam a configuração e execução do TnfSample.

1. Instale o SQL Server ou obtenha uma connection string válida
2. Crie uma base de dados (nome sugerido TnfZero)
3. Configure a conexão com o bando de dados, alterando os arquivos abaixo:
   - appsettings.Development.json no projeto Tnf.Architecture.Web
   - appsettings.json no projeto Tnf.Architecture.EntityFrameworkCore (configuração migrations)
   - Exemplo de ConnectionString: "Server=(localdb)\\MSSQLLocalDB;Database=TnfZero;Trusted_Connection=True;MultipleActiveResultSets=true"		
4. Acesso Package Manager Console no Visual Studio (Tools/Nuget Package Manager)
5. Execute a atualização do banco de dados baseado nas migrations
   - Update-Database -C LegacyDbContext -Project 'src\3 - Data\Tnf.Architecture.EntityFrameworkCore'	
	
## Messaging com RabbitMQ ##

O TnfSample-Architecture está implementado com uma solução de messaging com RabbitMQ.

###### Para habilitar: ######

1. Instale o RabbitMQ de https://www.rabbitmq.com/download.html ou obtenha uma conexão com RabbitMQ Válida
2. Descomente as linhas 87-91 do módulo em src/Tnf.Architecture.Web/Startup/WebModule.cs
3. Leia sobre o fluxo utilizado para o sample com messaging src/Tnf.Architecture.Application/Services/ProfessionalAppService.cs
