### Exemplos que serão vistos a seguir:

#### Os projeto utilizam TNF 2.0.2.7203

#### HelloWorld ####
Exemplo de criação de uma api com o minímo de infra-estrutura do Tnf contendo a configuração de localização por arquivo, log e registro de dependências:

- Pacotes que foram instalados:
	- Tnf.AspNetCore

#### BasicCrud ####
Exemplo de criação de dois CRUD's básicos utilizando EntityFrameworkCore e configuração de localização por arquivo, log e registro de dependências.

- Pacotes que foram instalados:
	- Tnf.Dto
	- Tnf.Repositories.AspNetCore
	- Tnf.Drivers.DevartOracle
	- Tnf.Domain
	- Tnf.AutoMapper
	- Tnf.EntityFrameworkCore
	
	- Microsoft.EntityFrameworkCore.SqlServer
	- Microsoft.EntityFrameworkCore.Sqlite
	- Devart.Data.Oracle.Entity.EFCore

#### Transactional ####
Exempo que contempla um cenário transacional:

- Pacotes que foram instalados:
	- Tnf.AspNetCore
	- Tnf.Repositories
	- Tnf.Specifications
	- Tnf.EntityFrameworkCore

#### RedisCache ####
Exemplo de um projeto usando o pacote do Redis do Tnf:

- Pacotes que foram instalados:
	- Tnf.Caching.Redis.JsonSerializer

#### Messaging ####
Exemplo de um projeto usando o pacote do RabbitMQ do Tnf:

- Pacotes que foram instalados:
	- Tnf.AspNetCore
	- Tnf.Bus.Queue.RabbitMQ
	- Tnf.Bus.Client
	- Tnf.Caching.Abstractions
	- Tnf.Notifications
	- Tnf.Kernel
	
	
	

	
#### Case3 ####
Exemplo de criação de uma aplicação utilizando o supporte a fila.
Este exemplo contém duas APIs separadas, onde uma fará o papel do Publish da mensagem e a outra do Subscriber. Para fins de visualização as mensagens recebidas pelo API que faz a sobrescrita persiste estas em um cache em memoria que pode ser consultado através do swagger.

- Pacotes que foram instalados:
	- Tnf.AspNetCore
	- Tnf.Bus.Queue.RabbitMQ
	- Tnf.Caching.Abstractions
	- Tnf.Bus.Client
	
#### Case4 ####
Exemplo de criação de uma aplicação utilizando a infra-estrutura de serviços de domínio com EntityFrameworkCore.
Para rodar a aplicação primeiro você deve rodar as migração executando o comando no seu Package Manager Console do visual studio 2017 para os contextos:
- Update-Database -Context:CustomerDbContext

- Pacotes que foram instalados:
	- Tnf.Domain
	- Tnf.AutoMapper
	- Tnf.EntityFrameworkCore
	- Tnf.Repositories.AspNetCore

#### Case5 ####
Exemplo do suporte ao banco de dados Oracle, utilizando dapper e EntityFramework Core.

##### Executar #####
- Configurar a connection string nos arquivos appsettings.json dos projetos Case5.Web e Case5.Infra
- Criar a base de dados no Oracle
- Rodar migração dos contextos CustomerDbContext, EmployeeDbContext, LocalizationDbContext e SettingDbContext

> O schema padrão do EntityFramework Core é **DBO**. Caso o schema criado para o banco de dados não seja DBO, é necessário alterar os arquivos abaixo:
- CustomerDbContext.cs 
- EmployeeDbContext.cs 
- LocalizationDbContext.cs 
- SettingDbContext.cs

Dicas sobre Oracle XE
> Instalar utilizando um usuário e senha local (Windows)
