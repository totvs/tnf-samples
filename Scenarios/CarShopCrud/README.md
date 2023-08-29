# Projeto CarShop

## Descrição

O projeto CarShop é uma aplicação de gerenciamento de loja de carros que permite aos usuários interagir com dados de carros, concessionárias e clientes.

## Funcionalidades

### Carros

- Adicionar novo carro
- Atualizar detalhes do carro
- Buscar detalhes de um carro específico
- Listar todos os carros
- Deleção de carro

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

### Tabela FIPE

- Enriquecimento de dados da tabela FIPE através de mensageria para atualização de preço e modelo dos carros na loja CarShop.

## Instalação

1. **Clone o repositório**:

   ```bash
   git clone [url-do-repositório]

   ```

2. Navegue até a pasta do projeto:

   ```bash
   cd CarShop

   ```

3. **Instale as dependências:**

   ```bash
   dotnet restore

   ```

4. **Execute o projeto:**
   ```bash
   dotnet run
   ```

## Dependências

- .NET Core 6.0
- Microsoft.Extensions.Logging
- Serilog
- Npgsql.EntityFrameworkCore.PostgreSQL
- Tnf.AspNetCore
- Tnf.AspNetCore.Security
- Tnf.Commands
- Tnf.Commands.FluentValidation
- Tnf.Messaging
- Tnf.Messaging.RabbitMQ
- Tnf.Repositories
- Tnf.EntityFrameworkCore
- Tnf.Kernel

Obs: Pacotes TNF na versão 6.3

# Projeto Tnf.CarShop

O projeto Tnf.CarShop é uma solução de gestão de loja de carros, desenvolvida com base no framework .NET Core. Abaixo estão descritas as camadas e responsabilidades de cada projeto dentro da solução:

## Camadas

### `Tnf.CarShop.Host`

- **Responsabilidade:** Este é o projeto de hospedagem, responsável por inicializar e configurar a aplicação. É nele que estão configurados os endpoints da API. Ao receber uma requisição, os endpoints enviam comandos para a camada de aplicação. Além disso, ele configura o pipeline de solicitações, as dependências da aplicação, gerencia a autenticação e também inclui a configuração do Swagger para documentação da API.

### `Tnf.CarShop.Application`

- **Responsabilidade:** Contém a lógica de aplicação e define a interface entre a camada de apresentação e o domínio. Esta camada é onde estão definidos os "command handlers", que são responsáveis por lidar com os comandos enviados pela camada de hospedagem e orquestrar as operações correspondentes no domínio. Além disso, possui os validadores para cada comando, garantindo que a lógica de negócio seja respeitada.

  Para organizar os comandos de cada entidade, foi adotada a seguinte estrutura de diretórios:

  ```
  Commands
  ├───Car
  │   ├───CarCommand
  │   ├───CarCommandHandler.cs
  │   └───CarCommandValidator.cs
  ├───Customer
  │   ├───CustomerCommand.cs
  │   ├───CustomerCommandHandler.cs
  │   ├───CustomerCommandValidator.cs
  ├───Fipe
  │   ├───FipeCommand.cs
  │   ├───FipeCommandHandler.cs
  │   ├───FipeCommandValidator.cs
  ├───Purchase
  │   ├───PurchaseCommand.cs
  │   ├───PurchaseCommandHandler.cs
  │   ├───PurchaseCommandValidator.cs
  ├───Store
  │   ├───StoreCommand.cs
  │   ├───StoreCommandHandler.cs
  │   ├───StoreCommandValidator.cs
  ```

  Essa estruturação garante que os comandos, handlers e validadores de cada entidade estejam organizados de maneira clara e modularizada, facilitando a manutenção e o entendimento do código.

`Tnf.CarShop.Application.Consumers`

Esta seção contém os consumers que lidam com a recepção de mensagens e desencadeiam a execução de comandos correspondentes:

`ApplyFipeTableConsumer`:

Responsabilidade: Responsável por consumir eventos do tipo CloudEvent<ApplyFipeTable>. Ao receber uma nova mensagem desse tipo, o consumer extrai a mensagem e os dados associados. Em seguida, constrói e envia o `ApplyFipeTableCommand` com as informações extraídas. Dependendo do sucesso da operação, um log correspondente é gerado.

Detalhes de Implementação:

csharp

```csharp
public class ApplyFipeTableConsumer : IConsumer<CloudEvent<ApplyFipeTable>>
{
...
public async Task ConsumeAsync(IConsumeContext<CloudEvent<ApplyFipeTable>> context, CancellationToken cancellationToken = default)
{
...
}
}
```

`Localization`

Responsável pelos arquivos de resource, enumeradores e idiomas que serão utilizadas em mensagens de validação do *TnfNotification* que são implementadas nos *CommandValidators* de cada comando ou utilizadas de forma independente, como por exemplo, nas *Controllers*.

```
`Tnf.CarShop.Application`
└───`Localization`
   │
   ├───`Sources`
   │   └───`CarShop-pt-BR.json`
   │
   ├───`LocalizationKeys.cs`
   ├───`LocalizationServiceCollection.cs`
   └───`LocalizationSource.cs`
```

`Messages`

```
`Tnf.CarShop.Application`
└───`Messages`
    │
    ├───`Events`
    │   ├───`CarCreatedEvent`
    │   ├───`CarEvent`
    │   ├───`CarEventPublisher`
    │   ├───`CarUpdatedEvent`
    │   └───`ICarEventPublisher`
    │
    ├───`ApplyFipeTable`
    ├───`Constants`
    └───`IOutputMessage`
```

Sobre a utilização do pacote:

As mensagens dentro de Tnf.CarShop.Application > Messages > Events utilizam o pacote Tnf.Messaging.

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
