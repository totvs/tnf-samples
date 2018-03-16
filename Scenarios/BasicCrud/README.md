## Basic CRUD Sample

Exemplo que contempla um cenário de CRUD básico acessando um banco SqlServer, SqLite e Oracle.

Segue abaixo uma explição resumida de cada camada da aplicação usando como exemplo o CRUD de Customer. 


### Camada de DTO

Essa camada guarda os objetos que a api irá receber e mandar para respeitar o Guia de implementação das APIs TOTVS (http://tdn.totvs.com/pages/releaseview.action?pageId=271660444).

#### Configuração de DTO

Esse camada precisa fazer referência as dll's <b>Tnf.Dto</b>, para utilizar os objetos de DTO disponibilizados pelo TNF.

#### Objetos de DTO

Objetos que a api irá receber ou enviar precisam implementar a interface Tnf.Dto.IDto ou herdar de sua classe abstrata Tnf.Dto.DtoBase, como mostra o exemplo abaixo:

``` c#
public class CustomerDto : DtoBase<Guid>
{
  public static CustomerDto NullInstance = new CustomerDto().AsNullable<CustomerDto, Guid>();
  
  public string Name { get; set; }
}
```

Objetos de configurações que iram retornar uma lista (como por exemplo um GetAll) e possuirão um cmapo de busca precisam implementar a interface Tnf.Dto.IRequestAllDto ou herdar de sua classe abstrata Tnf.Dto.RequestAllDto, como mostra o exemplo abaixo:

``` c#
public class CustomerRequestAllDto : RequestAllDto
{
  public string Name { get; set; }
}
```


### Camada de Web

Essa camada possui a responsabilidade de levantar a sua aplicação, configurar suas preferências (ex.: Log), registrar suas dependências e expôr suas api's.

#### Configuração de Web

Esse camada precisa fazer referência as dll's <b>Tnf.Repositories.AspNetCore</b>, para utilizar o gerenciamento de api, classes de controllers e o pattern de UnitOfWork implícito.

Para que este exemplo funcione você precisa configurar uma instancia válida do SqlServer, SqLite ou Oracle nos config da aplicação nos arquivos appsettings.Development.json e appsettings.Production.json.
	
Obs: Este exemplo utiliza as migrações do EntityFrameWorkCore e para que elas possam ser executadas o usuário de seu SqlServer, SqLite ou Oracle irá precisar de permissão para alterar a base de dados.

Para registrar as dependências da aplicação na classe Startup precisa chamar o registro de dependências de cada camada, como mostra o exemplo abaixo:

``` c#
public class Startup
{
  public IServiceProvider ConfigureServices(IServiceCollection services)
  {
    services
      .AddApplicationServiceDependency()  // dependencia da camada BasicCrud.Application
      .AddSqlServerDependency()           // dependencia da camada BasicCrud.Infra.SqlServer
      .AddTnfAspNetCore();                // dependencia do pacote Tnf.AspNetCore

    return services.BuildServiceProvider();
  }
}
```

Para usar o pattern de UnitOfWork implícito e registrar suas configurações de localização também na classe Startup precisa configurar o método Configure, como mostra o exemplo abaixo:

``` c#
public class Startup
{
  public void Configure(IApplicationBuilder app, IHostingEnvironment env)
  {
    // Configura o use do AspNetCore do Tnf
    app.UseTnfAspNetCore(options =>
    {
      // Adiciona as configurações de localização da aplicação
      options.UseDomainLocalization();
      
      // Recupera a configuração da aplicação
      var configuration = options.Settings.FromJsonFiles(env.ContentRootPath, "appsettings.json");
      
      // Configura a connection string da aplicação
      options.DefaultNameOrConnectionString = configuration.GetConnectionString(SqlServerConstants.ConnectionStringName);
    });
    
    // Habilita o uso do UnitOfWork em todo o request
    app.UseTnfUnitOfWork();
    
    app.UseMvc(routes =>
    {
      routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
    });
    
    app.Run(context =>
    {
      context.Response.Redirect("/swagger/ui");
      return Task.CompletedTask;
    });
  }
}
```

#### Configuração das Controllers

Para expôr uma api é necessario apenas que ela herde da classe Microsoft.AspNetCore.Mvc.TnfController, para que essa api respeite o padrão do Guia de implementação das APIs TOTVS ela precisa receber nos métodos as nossas interfaces de IRequestDto, IRequestAllDto e IDto, para que ele retorne também nesse padrão uma mensagem de erro ou sucesso é necessári oque cada método retorne o CreateResponse de cada verbo, como mostra o exemplo abaixo:

```c#
[Route(WebConstants.CustomerRouteName)]
public class CustomerController : TnfController
{
  private readonly ICustomerAppService appService;
  private const string name = "Customer";
  
  public CustomerController(ICustomerAppService appService)
  {
    this.appService = appService;
  }
  
  [HttpGet]
  public async Task<IActionResult> GetAll([FromQuery]CustomerRequestAllDto requestDto)
  {
    var response = await appService.GetAll(requestDto);
  
    return CreateResponseOnGetAll(response, name);
  }
  
  [HttpGet("{id}")]
  public async Task<IActionResult> Get(Guid id, [FromQuery]RequestDto<Guid> requestDto)
  {
    requestDto.WithId(id);
  
    var response = await appService.Get(requestDto);
  
    return CreateResponseOnGet<CustomerDto, Guid>(response, name);
  }
  
  [HttpPost]
  public async Task<IActionResult> Post([FromBody]CustomerDto customerDto)
  {
    customerDto = await appService.Create(customerDto);
  
    return CreateResponseOnPost<CustomerDto, Guid>(customerDto, name);
  }
  
  [HttpPut("{id}")]
  public async Task<IActionResult> Put(Guid id, [FromBody]CustomerDto customerDto)
  {
    customerDto = await appService.Update(id, customerDto);
  
    return CreateResponseOnPut<CustomerDto, Guid>(customerDto, name);
  }
  
  [HttpDelete("{id}")]
  public async Task<IActionResult> Delete(Guid id)
  {
    await appService.Delete(id);
  
    return CreateResponseOnDelete<CustomerDto, Guid>(name);
  }
}
```


### Camada de Application

Essa camada possui a responsabilidade de validar a integridade dos dados que o endpoint da solução recebe.

#### Configuração de Application

Como todas camadas é preciso registrar as suas dependências, contratos e implementações, para isso é ncessário criar um método que extenda (ou receba) a interface Microsoft.Extensions.DependencyInjection.IServiceCollection para que as camadas que possuem dependência para essa camada possam registrá-la, como mostra o exemplo abaixo:

```c#
public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddApplicationServiceDependency(this IServiceCollection services)
  {
    // Dependência do projeto BasicCrud.Domain
    services.AddDomainDependency();
    
    // Para habilitar as convenções do Tnf para Injeção de dependência (ITransientDependency, IScopedDependency, ISingletonDependency)
    // descomente a linha abaixo:
    // services.AddTnfDefaultConventionalRegistrations();
    
    // Registro dos serviços
    services.AddTransient<ICustomerAppService, CustomerAppService>();
    
    return services;
  }
}
```

Obs: Este exemplo não utiliza registro de dependência por convenção, ele registra todas as suas interfaces e implementações, para habilitar a convenção em cada método de dependência de cada camada é preciso adicionar a linha comentada acima e nas interfaces registradas por conveção precisam herdar de Tnf.Dependency.ITransientDependency, Tnf.Dependency.IScopedDependency ou Tnf.Dependency.ISingletonDependency de cordo com o seu tempo de vida na aplicação.


### Camada de Domain

Essa camada possui a responsabilidade de aplicar as regras de negócio da aplicação.

#### Configuração de Domain

Esse camada precisa fazer referência as dll's <b>Tnf.Domain</b>, para utilizar os pattern's de Builder e Specification e configurações de localização.

Para registra as dependências da camada de domínio sendo elas usando domínio padrão do TNF ou customizado, é ncessário criar um método que extenda (ou receba) a interface Microsoft.Extensions.DependencyInjection.IServiceCollection para que as camadas que possuem dependência para essa camada possam registrá-la, como mostra o exemplo abaixo:

```c#
public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddApplicationServiceDependency(this IServiceCollection services)
  {
    // Adiciona as dependencias para utilização dos serviços de crud generico do Tnf
    services.AddTnfDomain();
    
    // Para habilitar as convenções do Tnf para Injeção de dependência (ITransientDependency, IScopedDependency, ISingletonDependency)
    // descomente a linha abaixo:
    // services.AddTnfDefaultConventionalRegistrations();
    
    // Registro dos serviços
    services.AddTransient<IProductDomainService, ProductDomainService>();
    
    return services;
  }
}
```

#### Configuração de Localização

Para configurar a localização na sua aplicação é necessário criar um método que extenda (ou receba) a interface Tnf.Configuration.ITnfConfiguration para que as camadas que forem depender dessa possam registrar essa configuração, como mostra o exemplo abaixo:

```c#
public static class LocalizationExtensions
{
  public static void UseDomainLocalization(this ITnfConfiguration configuration)
  {
    // Incluindo o source de localização
    configuration.Localization.Sources.Add(
      new DictionaryBasedLocalizationSource(DomainConstants.LocalizationSourceName,
      new JsonEmbeddedFileLocalizationDictionaryProvider(
        typeof(DomainConstants).Assembly, 
        "BasicCrud.Domain.Localization.SourceFiles")));
    
    // Incluindo suporte as seguintes linguagens
    configuration.Localization.Languages.Add(new LanguageInfo("pt-BR", "Português", isDefault: true));
    configuration.Localization.Languages.Add(new LanguageInfo("en", "English"));
  }
}
```

#### Criação de Entidades utilizando os pattern's Builder e Specification

A camada de domínio não pode depender de nenhuma outra camada pois nela está a regra de negócio e se no futuro as outras camadas forem trocadas ou sofrerem alterações as suas regras de negócio devem ficar intactas na camada de domínio. 

Essa camada precisa saber criar suas entidades de negócio e aplicar regras de validação para seu negócio, para resolver isso o TNF faz uso dos pattern's de Builder e Specification.

Entidades de negócio precisam implementar a interface Tnf.Repositories.Entities.IEntity ou herdar de sua classe abstrata Tnf.Repositories.Entities.Entity.

Recomendamos que a entidade de domínio possua o builder que irá construí-la e que só possa construir essa entidade através desse Builder, para que não possa criar uma entidade sem ter sido aplicado suas regras de negócio, esse builder precisa implementar a interface Tnf.Builder.IBuilder ou herdar de sua classe abstrata Tnf.Builder.Builder, como mostra o exemplo abaixo:

``` c#
public class Customer : Entity<Guid>
{
  public string Name { get; internal set; }
  
  public enum Error
  {
    CustomerShouldHaveName
  }
  
  public static CustomerBuilder Create(INotificationHandler handler)
    => new CustomerBuilder(handler);
  
  public static CustomerBuilder Create(INotificationHandler handler, Customer instance)
    => new CustomerBuilder(handler, instance);
  
  public class CustomerBuilder : Builder<Customer>
  {
    public CustomerBuilder(INotificationHandler notificationHandler)
      : base(notificationHandler)
    {
    }
  
    public CustomerBuilder(INotificationHandler notificationHandler, Customer instance)
      : base(notificationHandler, instance)
    {
    }
  
    public CustomerBuilder WithId(Guid id)
    {
      Instance.Id = id;
      return this;
    }
  
    public CustomerBuilder WithName(string name)
    {
      Instance.Name = name;
      return this;
    }
  
    protected override void Specifications()
    {
      AddSpecification<CustomerShouldHaveNameSpecification>();
    }
  }
}
```

Podemos notar que adicionamos a especificação CustomerShouldHaveNameSpecification no builder de customer, essa especificação vai ser executada ao chamar o build da classe e se ela não for satisfeita ela levantará uma notificação com a chave passada para ela, essa especificação precisa implementar a interface Tnf.Specifications.ISpecification ou herdar de alguma de suas abstratas como Tnf.Specifications.Specification, como mostra o exemplo abaixo:

```c#
public class CustomerShouldHaveNameSpecification : Specification<Customer>
{
  public override string LocalizationSource { get; protected set; } = DomainConstants.LocalizationSourceName;
  public override Enum LocalizationKey { get; protected set; } = Customer.Error.CustomerShouldHaveName;
  
  public override Expression<Func<Customer, bool>> ToExpression()
  {
    return (p) => !string.IsNullOrWhiteSpace(p.Name);
  }
}
```

#### Injeção de domínio e repositório padrão do TNF (CRUD de Customer):
	
No CRUD de Customer criamos um exemplo de como utilizar a injeção de domínio e repositório padrão do TNF onde o desenvolvedor não precisa configurar uma classe de domínio nem uma classe de repositório para sua aplicação, e suas regras de negócio seriam executadas através dos pattern's de Specification e Builder.

Para isso a sua camada de Application precisa injetar a interface Tnf.Domain.Services.IDomainService explicitando a entidade de negócio e sua chave primária, como mostra o exemplo abaixo:

``` c#
public class CustomerAppService : ApplicationService, ICustomerAppService
{
  private readonly IDomainService<Customer, Guid> service;
  
  public CustomerAppService(IDomainService<Customer, Guid> service, INotificationHandler notificationHandler)
 	 : base(notificationHandler)
  {
    this.service = service;
  }
}
```

Fazendo isso o TNF irá injetar uma classe de domínio própria com os métodos de CRUD síncrono e assíncrono respeitando o pattern de Builder.

#### Injeção de domínio e repositório customizada (CRUD de Product):
	
No CRUD de Product criamos um exemplo de como utilizar a injeção de domínio e repositório customizada onde o desenvolvedor configura uma implementação de domínio e de repositório para sua aplicação e pode executar suas regras de negócio na classe de domínio e as regras de propriedades utilizando os pattern's de Specification e Builder.


### Camada de Infra

Essa camada possui a responsabilidade de fazer o gerenciamento do banco, ou seja, fazer a integração com o banco de dados de sua preferência, mapear as entidades que irão ser persisitidas e buscadas no banco e armazenar a lógica de persistência e busca.

Obs: Nesse resumo mostraramos como faz a configuração dessa camada usando nossa abstração para SqlServer.

#### Configuração de Infra

Esse camada precisa fazer referência as dll's <b>Tnf.AutoMapper</b>, para utilizar o mapeamento automático que o TNF disponibiliza, e <b>Tnf.EntityFrameworkCore</b>, para utilizar o EntityFramework para aplicar a lógica de persistência e busca ao banco de dados.

Para fazer com que essa camada utilize as configurações do EntityFrameworkCore do TNF é necessário chamar o método AddTnfEntityFrameworkCore da interface Microsoft.Extensions.DependencyInjection.IServiceCollection, como mostra o exemplo abaixo:

```c#
public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddInfraDependency(this IServiceCollection services)
  {
    return services
      .AddTnfEntityFrameworkCore()    // Configura o uso do EntityFrameworkCore registrando os contextos que serão usados pela aplicação
      .AddMapperDependency();         // Configura o uso do AutoMappper
  }
}
```

Como todas camadas é preciso registrar as suas dependências, contratos e implementações, para isso é ncessário criar um método que extenda (ou receba) a interface Microsoft.Extensions.DependencyInjection.IServiceCollection para que as camadas que possuem dependência para essa camada possam registrá-la.

Nessa camada também é necessário informar o contexto que a aplicação irá usar e com qual string de conexão, como mostra o exemplo abaixo:

```c#
public static IServiceCollection AddSqlServerDependency(this IServiceCollection services)
{
  services
    .AddInfraDependency()
    .AddTnfDbContext<BasicCrudDbContext>((config) =>
    {
      if (config.ExistingConnection != null)
        config.DbContextOptions.UseSqlServer(config.ExistingConnection);
      else
        config.DbContextOptions.UseSqlServer(config.ConnectionString);
    });


  // Registro dos repositórios
  services.AddTransient<IProductRepository, ProductRepository>();
  services.AddTransient<IProductReadRepository, ProductReadRepository>();

  return services;
}
```

Obs: Nesse exemplo é mostrado como se configura o contexto com o banco de dados SqlServer por isso ao adicionar a string de conexão usamos o método UseSqlServer que está na dll <b>Microsoft.EntityFrameworkCore.SqlServer</b>, se estiver usando Oracle o método será UseOracle da dll <b>Devart.Data.Oracle.Entity.EFCore</b> e se estiver usando SqLite o método será UseSqlite da dll <b>Microsoft.EntityFrameworkCore.Sqlite</b>.
 
#### Configuração de Mapeamento

Para configurar o mapeamento na sua aplicação é necessário criar um método que extenda (ou receba) a interface Tnf.Configuration.ITnfConfiguration para que as camadas que forem depender dessa possam registrar essa configuração, como mostra o exemplo abaixo:

```c#
public static class MapperExtensions
{
  public static IServiceCollection AddMapperDependency(this IServiceCollection services)
  {
    // Configura o uso do AutoMappper
    return services.AddTnfAutoMapper(config =>
    {
      config.AddProfile<BasicCrudProfile>();
    });
  }
}
```

Abaixo segue o exemplo do profile que mostra o mapeamento das entidades:

```c#
public class BasicCrudProfile : Profile
{
  public BasicCrudProfile()
  {
    CreateMap<Customer, CustomerDto>();
  }
}
```

Obs: Na configuração de mapeamentos usamos o framework AutoMapper para isso, se precisar fazer um mapeamento mais complexo segue a documentação: http://docs.automapper.org/en/stable/Configuration.html.

#### Configuração de Contexto

O contexto vai definir as tabelas do banco da aplicação e o mapeamento do objeto da aplicação para o registro do banco, como mostra o exemplo abaixo:

```c#
public class BasicCrudDbContext : TnfDbContext
{
  public DbSet<Customer> Customers { get; set; }

  // Importante o construtor do contexto receber as opções com o tipo generico definido: DbContextOptions<TDbContext>
  public BasicCrudDbContext(DbContextOptions<BasicCrudDbContext> options, ITnfSession session) 
    : base(options, session)
  {
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.ApplyConfiguration(new CustomerTypeConfiguration());
  }
}
```

Para configurar o mapeamento do objeto da aplicação para a tabela do banco de dados recomendamos que usem a estratégia do próprio EntityFramework de separar em uma classe que implementa a interface Microsoft.EntityFrameworkCore.IEntityTypeConfiguration, como mostra o exemplo abaixo:

```c#
public class CustomerTypeConfiguration : IEntityTypeConfiguration<Customer>
{
  public void Configure(EntityTypeBuilder<Customer> builder)
  {
    builder.HasKey(k => k.Id);
    builder.Property(p => p.Name).IsRequired();
  }
}
```

#### Utilização de Migrations

Nesse exemplo usamos a tática de modelagem de Code First, onde escrevemos o código para gerar as tabelas do banco em cima dele, com EntityFramework fazemos uso de migration's que atualizam o banco para nós, para isso precisamos criar um Factory que será chamado ao executar a migration e retornará uma instância do contexto, como mostra o exemplo abaixo:

```c#
public class BasicCrudDbContextFactory : IDesignTimeDbContextFactory<BasicCrudDbContext>
{
  public BasicCrudDbContext CreateDbContext(string[] args)
  {
    var builder = new DbContextOptionsBuilder<BasicCrudDbContext>();

    var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json", true)
                .Build();
    
    builder.UseSqlServer(configuration.GetConnectionString(SqlServerConstants.ConnectionStringName));

    return new BasicCrudDbContext(builder.Options, NullTnfSession.Instance);
  }
}
```

Para executar migrations agora é necessário abrir a janela do Package Manager Console dentro de seu Visual Studio, colocar o seu projeto de endpoint como StartUp Project, no nosso caso seria o projeto BasicCrud.Web, selecionar o Default Project para o projeto que possui o Factory e o DbContext, no nosso caso seria o projeto BasicCrud.Infra.SqlServer, e executar o seguinte comando:

	Add-Migration "NomeDaMigration"

Fazendo isso a migration será criada e falta apenas atualizar o banco de dados com o seguinte comando:

	Update-Database
