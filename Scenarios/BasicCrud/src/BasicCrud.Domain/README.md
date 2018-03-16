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