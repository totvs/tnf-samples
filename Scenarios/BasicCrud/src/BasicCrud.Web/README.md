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