### Transactional Sample

#### Este exemplo contempla um cenário transacional.

Para que este exemplo funcione você precisa ter o LocalDb instalado em seu visual studio ou configurar uma instancia válida do SqlServer
nos config da aplicação no projeto
	
	Transactional.Web 
		appsettings.Development.json e 
		appsettings.Production.json.
	
Obs: Este exemplo utiliza as migrações do EntityFrameWorkCore e para que elas possam ser executadas o usuário de seu SqlServer irá precisar de permissão para alterar a base de dados.

#### Existem duas formas de controlar esse comportamento no TNF:
	
* **Manual:** de forma explicita no código através da injeção da interface IUnitOfWorkManager que possui
  métodos para iniciar ".Begin()" e ".Complete" para comitar as alterações;

* **Automático:** de forma implícita a utilização desta opção acarreta na criação de um UnitOfWork para cada request realizado.
  Isso se dá ao fato da utilização do pacote Tnf.Repositories.AspNetCore que possui além das dependências
  de AspNetCore, um middleware que pode ser chamado no Startup de sua aplicação ".UseTnfUnitOfWork()" para ser adicionado ao pipeline de sua API 
  garantindo que cada request tenha um Unit Of Work presente.

#### Dependendo do TransactionScopeOption utilzado na criação de um Uow através do método ".Begin()" o controle transacional funcionará com descrito a seguir:

- **Required:** Uma transação é exigida. Ele irá criar uma transação se esta não existir ainda. Caso você crie transações aninhandas com o mesmo
  TransactionScopeOption definido para Required o Unit Of Work não irá criar mais transações e manterá apenas a que foi criada anteriormente para comitá-la ao final
  deste escopo. Este é o valor default ao criar um Unit Of Work

- **RequiresNew:** Uma transação nova sempre será criada para aquele escopo.

- **Suppress:** O contexto da transação é suprimido. Todas as operações dentro deste escopo serão feitos sem um contexto de transação.

#### Para alterar esse valores a nível de aplicação você deve configurar o UnitOfWorkOptions. Isso pode ser feito de duas maneiras:

- Através do Startup de sua aplicação, configurando através do método .UseTnfAspNetCore:
	
```
public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILogger<Startup> logger)
{
	app.UseTnfAspNetCore(options =>
	{
		// ---------- Configurações de Unit of Work a nível de aplicação

		// Por padrão um Uow é transacional: todas as operações realizadas dentro de um Uow serão
		// comitadas ou desfeitas em caso de erro
		options.UnitOfWorkOptions().IsTransactional = true;

		// IsolationLevel default de cada transação criada. (Precisa da configuração IsTransactional = true para funcionar)
		options.UnitOfWorkOptions().IsolationLevel = IsolationLevel.ReadCommitted;

		// Escopo da transação. (Precisa da configuração IsTransactional = true para funcionar)
		options.UnitOfWorkOptions().Scope = TransactionScopeOption.Required;

		// Timeout que será aplicado (se este valor for informado) para toda nova transação criada
		// Não é indicado informar este valor pois irá afetar toda a aplicação.
		options.UnitOfWorkOptions().Timeout = TimeSpan.FromSeconds(5);

		// ----------
	});
}
```

- Outra opção é acessar via uma extensão baseado no IServiceProvider:

```
public void Configure(IServiceProvider provider)
{
	// ---------- Configurações de Unit of Work a nível de aplicação

	// Por padrão um Uow é transacional: todas as operações realizadas dentro de um Uow serão
	// comitadas ou desfeitas em caso de erro
	provider.ConfigureTnf().UnitOfWorkOptions().IsTransactional = true;

	// IsolationLevel default de cada transação criada. (Precisa da configuração IsTransactional = true para funcionar)
	provider.UnitOfWorkOptions().IsolationLevel = IsolationLevel.ReadCommitted;

	// Escopo da transação. (Precisa da configuração IsTransactional = true para funcionar)
	provider.UnitOfWorkOptions().Scope = TransactionScopeOption.Required;

	// Timeout que será aplicado (se este valor for informado) para toda nova transação criada
	provider.UnitOfWorkOptions().Timeout = TimeSpan.FromSeconds(5);

	// ----------
}
```

Neste exemplo é contemplado apenas o cenário Manual, onde todo acesso a um Repository está sendo criado um Unit Of Work de forma explicíta.
