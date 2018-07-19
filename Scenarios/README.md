### Exemplos que serão vistos a seguir:

#### Os projeto utilizam TNF 2.0.2.8601

Todos os exemplos a seguir estão com o swagger configurado, log (Serilog) e compactação habilitados.

#### [HelloWorld](https://github.com/totvsnetcore/tnf-samples/tree/master/Scenarios/HelloWorld) ####
Exemplo de criação de uma api com o minímo de infra-estrutura do Tnf contendo a configuração de localização por arquivo e registro de dependências:

- Pacotes que foram instalados:
	- Tnf.AspNetCore

#### [BasicCrud](https://github.com/totvsnetcore/tnf-samples/tree/master/Scenarios/BasicCrud) ####
Exemplo de criação de dois CRUD's básicos utilizando EntityFrameworkCore e configuração de localização por arquivo e registro de dependências.

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

#### [SuperMarket](https://github.com/totvsnetcore/tnf-samples/tree/master/Scenarios/SuperMarket) ####
Exemplo de criação de criação e gerenciamento de um pedido utilizando o TNF com Redis, Rabbitmq, SqlServer, EntityFrameworkCore, configuração de localização por arquivo e registro de dependências.

- Pacotes que foram instalados:
	- Tnf.AspNetCore
	- Tnf.Dto
	- Tnf.Repositories.AspNetCore
	- Tnf.Drivers.DevartOracle
	- Tnf.Domain
	- Tnf.AutoMapper
	- Tnf.EntityFrameworkCore	
	- Tnf.Bus.Queue.RabbitMQ
	- Tnf.Caching.Redis.JsonSerializer

#### [Querying](https://github.com/totvsnetcore/tnf-samples/tree/master/Scenarios/Querying) ####
Exemplo que consultas através do repositório do TNF:

- Pacotes que foram instalados:
	- Tnf.Repositories.AspNetCore
	- Tnf.EntityFrameworkCore	

#### [Dapper](https://github.com/totvsnetcore/tnf-samples/tree/master/Scenarios/Dapper) ####
Exemplo que consultas através do repositório do TNF usando Dapper:

- Pacotes que foram instalados:
	- Tnf.Repositories.AspNetCore
	- Tnf.AutoMapper	
	- Tnf.Dapper	

#### [Transactional](https://github.com/totvsnetcore/tnf-samples/tree/master/Scenarios/Transactional) ####
Exemplo que contempla um cenário transacional:

- Pacotes que foram instalados:
	- Tnf.AspNetCore
	- Tnf.Repositories
	- Tnf.Specifications
	- Tnf.EntityFrameworkCore

#### [RedisCache](https://github.com/totvsnetcore/tnf-samples/tree/master/Scenarios/RedisCache) ####
Exemplo de um projeto usando o pacote do Redis do Tnf:

- Pacotes que foram instalados:
	- Tnf.Caching.Redis.JsonSerializer

#### [Messaging](https://github.com/totvsnetcore/tnf-samples/tree/master/Scenarios/Messaging) ####
Exemplo de um projeto usando o pacote do RabbitMQ do Tnf:

- Pacotes que foram instalados:
	- Tnf.AspNetCore
	- Tnf.Bus.Queue.RabbitMQ
	- Tnf.Bus.Client
	- Tnf.Caching.Abstractions
	- Tnf.Notifications
	- Tnf.Kernel

#### [ProdutoXyz](https://github.com/totvs/tnf-samples/tree/master/Scenarios/ProdutoXyz) ####
Exemplo de um projeto usando o pacote de segurança do Tnf

- Pacotes que foram instalados:
	- Tnf.AspNetCore.Security
	- Tnf.EntityFrameworkCore
	- Tnf.AutoMapper
	- Tnf.Dto
	- Tnf.Domain