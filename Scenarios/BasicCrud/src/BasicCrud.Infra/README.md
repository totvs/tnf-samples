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
