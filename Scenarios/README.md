### Exemplos que serão vistos a seguir:

#### Alguns cenários de utilização estão disponíveis em nosso repositório.
#### Case1 ####
Exemplo de criação de uma api com o minímo de infra-estrutura do Tnf contendo a configuração de localização por arquivo, log e registro de dependências:

- Pacotes que foram instalados:
	- Tnf.AspNetCore
	- Tnf.Notifications

#### Case2 ####
Exemplo de criação de uma aplicação utilizando EntityFrameworkCore com Dapper e configuração de localização via banco de dados. Para rodar essa aplicação é necessário configurar nos arquivos appsettings qual é a string de conexão que será utilizada (default é utizando o LocalDb).
Para rodar a aplicação primeiro você deve rodar as migração executando o comando no seu Package Manager Console do visual studio 2017 para os contextos:
- Update-Database -Context:CustomerDbContext
- Update-Database -Context:EmployeeDbContext
- Update-Database -Context:LocalizationDbContext
- Update-Database -Context:SettingDbContext

- Pacotes que foram instalados:
	- Tnf.AutoMapper
	- Tnf.Dapper
	- Tnf.EntityFrameworkCore
	- Tnf.Localization.EntityFrameworkCore
	- Tnf.Settings.EntityFrameworkCore
	- Tnf.Repositories.AspNetCore
	
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
1. CustomerDbContext.cs
2. EmployeeDbContext.cs
3. LocalizationDbContext.cs
4. SettingDbContext.cs

Dicas sobre Oracle XE
> Instalar utilizando um usuário e senha local (Windows)
