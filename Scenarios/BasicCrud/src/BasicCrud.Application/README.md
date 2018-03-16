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