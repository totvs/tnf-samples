# Projeto CarShop

## Descrição

O projeto CarShop é uma aplicação de gerenciamento de loja de carros que permite aos usuários interagir com dados de carros, concessionárias e clientes.

## Funcionalidades

### Carros
- Adicionar novo carro
- Atualizar detalhes do carro
- Buscar detalhes de um carro específico
- Listar todos os carros
- Deleção e carro

### Clientes
- Adicionar novo cliente
- Atualizar detalhes do cliente
- Buscar detalhes de um cliente específico
- Listar todos os clientes
- Deleção de cliente

### Concessionárias
- Adicionar nova concessionária
- Atualizar detalhes da concessionária
- Buscar detalhes de uma concessionária específica
- Listar todas as concessionárias
- Deleção de concessionária

### Compras
- Gerenciamento de Compras
- Registro de Compras
- Visualização de histórico de Compras
- Atualização de detalhes de Compra
- Deleção de registros de Compra

## Instalação

1. **Clone o repositório**:
   ```bash
   git clone [url-do-repositório]

2. Navegue até a pasta do projeto:
   ```bash
   cd CarShop

3. **Instale as dependências:**
   ```bash
   dotnet restore

4. **Execute o projeto:**
   ```bash
   dotnet run

## Dependências

- .NET Core 6.0
- Microsoft.Extensions.Logging
- TNF 6.3

# Projeto Tnf.CarShop

O projeto Tnf.CarShop é uma solução de gestão de loja de carros, desenvolvida com base no framework .NET Core. Abaixo estão descritas as camadas e responsabilidades de cada projeto dentro da solução:

## Camadas

### `Tnf.CarShop.Host`
- **Responsabilidade:** Este é o projeto de hospedagem, responsável por inicializar e configurar a aplicação. É nele que estão configurados os endpoints da API, que utilizam o padrão CQRS. Ao receber uma requisição, os endpoints enviam comandos para a camada de aplicação. Além disso, ele configura o pipeline de solicitações, as dependências da aplicação, gerencia a autenticação e também inclui a configuração do Swagger para documentação da API.

  
### `Tnf.CarShop.Application`
- **Responsabilidade:** Contém a lógica de aplicação e define a interface entre a camada de apresentação e o domínio. Esta camada é onde estão definidos os "command handlers", que são responsáveis por lidar com os comandos enviados pela camada de hospedagem e orquestrar as operações correspondentes no domínio. Além disso, possui os validadores para cada comando, garantindo que a lógica de negócio seja respeitada.

    Para organizar os comandos de cada entidade, foi adotada a seguinte estrutura de diretórios:

    ```
    Commands
    ├───car
    │   ├───Create
    │   │   ├───CreateCarCommand.cs
    │   │   ├───CreateCarCommandHandler.cs
    │   │   └───CreateCarCommandValidator.cs
    │   ├───Delete
    │   │   ├───DeleteCarCommand.cs
    │   │   ├───DeleteCarCommandHandler.cs
    │   │   └───DeleteCarCommandValidator.cs
    │   ├───Get
    │   │   ├───GetCarCommand.cs
    │   │   ├───GetCarCommandHandler.cs
    │   │   └───GetCarCommandValidator.cs
    │   └───Update
    │       ├───UpdateCarCommand.cs
    │       ├───UpdateCarCommandHandler.cs
    │       └───UpdateCarCommandValidator.cs
    ```

    Essa estruturação garante que os comandos, handlers e validadores de cada entidade estejam organizados de maneira clara e modularizada, facilitando a manutenção e o entendimento do código.

    **Factories de Conversão:** A camada de aplicação também conta com factories dedicadas para auxiliar na conversão entre DTOs e entidades. Essas factories implementam a interface `IFactory<TDto, TEntity>`, que define os métodos `ToEntity` e `ToDto` para realizar as respectivas conversões. 

    ```csharp
    namespace Tnf.CarShop.Application.Factories.Interfaces;

    public interface IFactory<TDto, TEntity>
    {
        TEntity ToEntity(TDto dto);
        TDto ToDto(TEntity entity);
    }
    ```

    Utilizando estas factories, a camada de aplicação pode converter facilmente entre DTOs e entidades, garantindo que a conversão esteja centralizada e padronizada em toda a aplicação.

    **Exemplos de Uso das Factories:**
    
    1. **Convertendo uma lista de entidades em uma lista de DTOs:**
    
       Supondo que `cars` seja uma lista de entidades do tipo `Car`, você pode converter essa lista inteira em uma lista de DTOs da seguinte forma:
       ```csharp
       var carsDto = cars.Select(_carFactory.ToDto).ToList();
       ```

    2. **Convertendo uma única entidade em um DTO:**
       
       Para converter uma entidade individual `car` em seu DTO correspondente:
       ```csharp
       var carDto = _carFactory.ToDto(car);
       ```

    3. **Convertendo um DTO de volta para uma entidade:**
       
       Se você tiver um `carDto` e desejar criar uma nova entidade `Car` a partir dele:
       ```csharp
       var newCar = _carFactory.ToEntity(carDto);
       ```

    Estes exemplos demonstram como as factories simplificam o processo de conversão entre DTOs e entidades, permitindo que essa lógica de conversão seja reutilizada e centralizada.

### `Tnf.CarShop.Domain`

**Responsabilidade:**  
Esta camada contém todas as entidades, eventos de domínio, lógica de negócios e interfaces de repositório. Ela representa o coração e a lógica central do sistema.

#### Domínio Rico
- Adotamos o conceito de **Domínio Rico** nesta camada. Isso significa que as entidades não são apenas simples conjuntos de dados, mas também encapsulam a lógica de negócios relacionada a elas. Em vez de apenas ter propriedades, as entidades nesta camada têm métodos que executam operações e aplicam regras de negócios. Isso facilita a manutenção, pois a lógica e os dados estão co-localizados, e garante que a lógica de negócios seja consistentemente aplicada sempre que uma entidade for usada.

#### Interfaces de Repositório
- A camada de domínio também define interfaces para repositórios que são responsáveis por acessar e persistir entidades. Estas interfaces herdam do `IRepository` fornecido pelo `Tnf.Repositories`, garantindo assim uma padronização e fornecendo um conjunto comum de operações para consulta e persistência.

### `Tnf.CarShop.EntityFrameworkCore`
- **Responsabilidade:** Implementa as interfaces do repositório definidas na camada de domínio usando o Entity Framework Core. Esta camada interage diretamente com o banco de dados.

### `Tnf.CarShop.EntityFrameworkCore.PostgreSql`
- **Responsabilidade:** Extensões e configurações específicas para o banco de dados PostgreSQL usando o Entity Framework Core.

### `Tnf.CarShop.EntityFrameworkCore.Migrator`
- **Responsabilidade:** Projeto responsável por executar migrações de banco de dados e atualizar o esquema do banco de dados.
