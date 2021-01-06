# SGDP

Exemplo de cenário utilizando o sdk Tnf.Sgdp (Sistema de Gestão de Dados Pessoais), responsável pela identificação, anonimização e auditoria no tratamento de dados pessoais. Este sdk fornece as ferramentas para acomodação das aplicações à LGPD.

## Dependências

O projeto utiliza um banco de dados PostgreSQL. A connection string (appsettings.json) deve ser definida de acordo com a instancia de DB a ser utilizada.
A aplicação realiza a integração com o serviço sgdp, utilizando RabbitMQ para envio de logs de auditoria de dados e para recebimento/envio de comandos/respostas de requisição de dados e mascaramento de informações. É necessário configurar os parametros de conexão ao RabbitMQ do sgdp-service e o endereço do serviço (remoto ou local).

## Modo de uso

### Identificação

A identificação de dados pessoais na aplicação é feita por meio de atributos. Estes podem ser utilizados nas entidades e nas classes que acessam dados pessoais.
A partir das classes identificadas com atributos é realizada a construção do metadado sgdp, que descreve todos os dados pessoais da aplicação e códigos (classes) que acessam tais dados.

#### SgdpAuditedEntity

Atributo simples utilizado para identificar a entidade como auditada. Logs de auditoria serão gerados para comandos executados sobre entidades marcadas com o atributo.

#### SgdpData

O atributo SGDPData deve ser utilizado nos campos que armazenam um dado pessoal. Possui as seguintes propriedades:

* Sensitive: identifica o dado como sensível;

* Type: define o tipo do dado pessoal (valor padrão EMPTY), com os valores possíveis:
    * CPF(Constants.CPF),
    * RG(Constants.RG),
    * EMAIL(Constants.EMAIL),
    * EMPTY(Constants.EMPTY);

* AllowsAnonymization: define que o dado pessoal pode ser anonimizado;

* Identification: define que o dado é um identificador pessoal.

#### SgdpPurpose

O atributo SGDPPurpose define o propósito (finalidade) do tratamento (armazenamento ou processamento) de um determinado dado pessoal. Pode ser utilizado multiplas vezes para o mesmo dado, indicando finalidades múltiplas. Possui as seguintes propriedades:

* Classification: define a classificação do dado pessoal (propriedade obrigatória), com os valores possíveis:
    * CONTRACT_EXECUTION,
    * LEGAL_OBLIGATION,
    * CONSENTMENT,
    * PUBLIC_POLICIES,
    * RESEARCH_STUDY,
    * REGULAR_EXERCISE_OF_LAW,
    * LIFE_PROTECTION,
    * HEALTH_PROTECTION,
    * LEGITIMATE_INTEREST,
    * CREDIT_PROTECTION(;
    
* Justification: define a justificativa para o armazenamento e tratamento do dado pessoal (propriedade obrigatória).

#### SgdpDescription

O atributo SGDPDescription deve ser utilizado para descrever uma entidade, mensagem ou atributo, facilitando a compreensão do metadado gerado.

Exemplo de utilização das anotações em uma classe de entidade:
```c#
[SgdpAuditedEntity]
[SgdpDescription("Classe que contem os dados do cliente")]
public class Customer : IEntity
{
    public Guid Id { get; set; }

    [SgdpData(true, SgdpDataTypes.CPF, false, false)]
    [SgdpPurpose(SgdpClassification.CREDIT_PROTECTION, "Usado para consultar nos sistemas de credito")]
    [SgdpDescription("CPF do cliente")]
    public string Cpf { get; set; }

    [SgdpData(false, SgdpDataTypes.Email, true, true)]
    [SgdpPurpose(SgdpClassification.CONSENTMENT, "Client consentiu em fornecer o email")]
    [SgdpDescription("Email do cliente")]
    public string Email { get; set; }

    [SgdpData(false, SgdpDataTypes.RG, true, true)]
    [SgdpPurpose(SgdpClassification.LEGAL_OBLIGATION, "Somos obrigados a guardar o RG dos clientes")]
    [SgdpDescription("RG do cliente")]
    public string Rg { get; set; }
}
```

### Metadado

O metadado da aplicação é construido pelo sdk Tnf.Sgdp a partir dos atributos e enviado para o sgdp-service durante o startup da aplicação. Exemplo de metadado:
```json
{
  "applicationId": "sgdp-metadata-applicationId",
  "productId": "SGDP Metadata ProductId",
  "models": {
    "br.com.sgdp.model.Customer": {
      "sgdpSupport": true,
      "description": "Cadastro de Clientes",
      "attributes": {
        "identification": {
          "type": "String",
          "description": "Identification",
          "sgdpData": {
            "sensitive": true,
            "type": "CPF",
            "allowsAnonymization": true,
            "identification": true
          },
          "sgdpPurposes": [
            {
              "classification": "REGULAR_EXERCISE_OF_LAW",
              "justification": "Numero de identificação do Cliente"
            }
          ]
        },
        "gender": {
          "type": "String",
          "description": "Gender",
          "sgdpData": {
            "sensitive": true,
            "type": "EMPTY",
            "allowsAnonymization": false,
            "identification": false
          },
          "sgdpPurposes": []
        },
        "id": {
          "type": "int",
          "description": null,
          "sgdpData": null,
          "sgdpPurposes": []
        }
      },
      "usedAt": [
        "br.com.sgdp.api.CustomerController"
      ]
    }
  },
  "codes": {
    "br.com.sgdp.api.CustomerController": {
      "description": "Validar o SGDP, sobre a identificação, auditoria e anonimização de dados pessoais de Clientes"
    }
  },
  "package": "br.com.sgdp"
}
```

### Auditoria

A LGPD define que consultas e tratamentos realizados sobre dados pessoais devem ser auditados. O Tnf.Sgdp implementa a auditoria por meio da interceptação das queries e comandos executados sobre as entidades, à exceção de consultas que utilizam projection (consultas que acessam determinadas entidades mas retornam um resultado de tipo diferente da(s) entidades da consulta).

Para as lógicas de tratamento de dados que não podem delegar a auditoria à interceptação, como é o caso de acesso a dados em cache ou das supracitadas projections, o registro deve ser feito através de chamada manual à API do logger, conforme abaixo:
```c#
await _sgdpLogger.LogAsync(<nome da ação>, <objeto com dado pessoal>, <tipo do objeto acessado>);
```

#### SgdpCode

O registro de tratamento de dados também deve informar ao sgdp-service qual código (classe) realizou a operação. O logger identifica de forma automatica o código de origem da operação, se valendo do atributo SgdpCode para isso. O atributo deve ser utilizado para identificar o código, conforme abaixo:
```c#
[SgdpCode("Validar o LGPD do TNF, sobre a identificação, auditoria e anonimização de dados pessoais")]
public class CompanyRepository : EfCoreRepositoryBase<OrderDbContext, Company>, ICompanyRepository
{
```

#### Logs de tratamento

Os registros de tratamento de dados (exemplo de log abaixo) são enviados via broker de mensageria (RabbitMQ) para o sgdp-service, responsável por armazenar os registros.
```json
{
  "uuid": "6bb76d63-18c4-42fa-a5cc-8bcbd576b8ce",
  "tenantId": "b6572633-5a42-4d54-b2bf-8b06e4b92325",
  "applicationId": "sgdp-metadata-applicationId",
  "timestamp": "2020-11-05T14:33:22.671623",
  "user": "admin",
  "code": "br.com.sgdp.api.CustomerController",
  "model": "br.com.sgdp.model.Customer",
  "ids": {},
  "action": "LOAD",
  "data": {
    "name": "Qui-Gon Jinn",
    "identification": "111.111.111-11",
    "email": "jinn@space.com",
    "gender": "male"
  }
}
```

### Consulta

#### SgdpDataCommand

O sgdp-service pode solicitar às aplicações os dados pessoais de um determinado titular. Esta solicitação é feita por meio de comandos enviados à aplicação via RabbitMQ. Exemplo de comando abaixo:
```json
{
  "header": {
    "type": "SGDPDataCommand",
    "tenantId": "b6572633-5a42-4d54-b2bf-8b06e4b92325",
    "generatedOn": "2020-11-05T14:45:53.000823-03:00",
    "locale": "en_GB"
  },
  "content": {
    "requestID": "c88a464b-21d1-416d-9116-0676c0ae52f9",
    "identifiers": {
      "CPF": "111.111.111-11"
    }
  }
}
```

#### SgdpDataService

O comando de consulta de dados é recebido pelo Tnf.Sgdp e delegado a um SgdpDataService definido pelo desenvolvedor da aplicação. O SgdpDataService deve implementar a interface ISgdpDataService e deve ser registrado durante a etapa de registro de serviços (método AddSgdpDataService<>()), juntamente com o serviço definido pelo usuário para tratamento de comandos de anonimização. Abaixo, exemplo do registro dos serviços:
```c#
public void ConfigureServices(IServiceCollection services)
{
    ...
    
    services.AddTnfSgdp(sgdp =>
    {
        sgdp.ConfigureOptions(Configuration)
            .AddSgdpDataService<SampleSgdpDataService>()
            .AddSgdpMaskService<SampleSgdpMaskService>()
            .UseEFCoreLogBuffer<OrderDbContext>(Configuration);
    });
    
    ...
}
```

O método ExecuteAsync definido pela interface ISgdpDataService recebe o contexto do comando de requisição de dados, contendo uma lista de identificadores de titular de dados. O desenvolvedor é responsável por buscar os dados a partir dos identificadores e, através do método AddData(object item) do contexto, acrescentá-los ao contexto. Por fim, o handler de mensagem subjacente do Tnf.Sgdp, que realizou a chamada do método ExecuteAsync, usa os dados informados pelo desenvolvedor para montar e enviar a resposta ao sgdp-service.
```c#
public class SampleSgdpDataService : ISgdpDataService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ITnfSession _session;

    public SampleSgdpDataService(ICustomerRepository customerRepository, ITnfSession session)
    {
        _customerRepository = customerRepository;
        _session = session;
    }

    public async Task ExecuteAsync(SgdpDataCommandContext context, CancellationToken cancellationToken = default)
    {
        var tenantId = _session.TenantId;

        var hasCpf = context.Identifiers.TryGetValue("CPF", out var cpf);
        var hasEmail = context.Identifiers.TryGetValue("EMAIL", out var email);
        var hasRg = context.Identifiers.TryGetValue("RG", out var rg);

        if (hasCpf || hasEmail || hasRg)
        {
            var data = await _customerRepository.GetCustomerAsync(
                customer => (cpf.IsNullOrEmpty() || customer.Cpf.Equals(cpf))
                    && (email.IsNullOrEmpty() || customer.Email.Equals(email))
                    && (rg.IsNullOrEmpty() || customer.Rg.Equals(rg))
                );

            if (data != null)
            {
                context.AddData(data);
            }
        }
    }
}
```

#### SgdpDataResponse

A mensagem de resposta enviada ao sgdp-service tem o formato abaixo:
```json
{
  "header": {
    "type": "SGDPDataResponse",
    "tenantId": "b6572633-5a42-4d54-b2bf-8b06e4b92325",
    "generatedOn": "2020-12-07T13:03:50.025192-03:00",
    "locale": "en_GB"
  },
  "content": {
    "requestID": "b8fc325d-db62-403b-b90b-fa1d167b0a41",
    "applicationId": "sgdp-metadata-applicationId",
    "response": {
      "customers": {
        "models": {
          "br.com.sgdp.model.Customer": [
            {
              "identification": "111.111.111-11",
              "email": "jinn@space.com",
              "gender": "male"
            }
          ]
        }
      }
    }
  }
}
```

### Anonimização

#### SgdpMaskCommand

O sgdp-service pode solicitar às aplicações que os dados pessoais de um determinado titular sejam anonimizados. Esta solicitação é feita por meio de comandos enviados à aplicação via RabbitMQ. Exemplo de comando abaixo:
```json
{
  "header": {
    "type": "SGDPMaskCommand",
    "tenantId": "b6572633-5a42-4d54-b2bf-8b06e4b92325",
    "generatedOn": "2020-11-05T14:48:44.000874-03:00",
    "locale": "en_GB"
  },
  "content": {
    "requestID": "d41f3032-2bb7-4472-aaad-ef74ddf0609c",
    "identifiers": {
      "CPF": "111.111.111-11"
    },
    "toVerify": true
  }
}
```

#### SgdpMaskService

O comando de anonimização de dados é recebido pelo Tnf.Sgdp e delegado a um SgdpMaskService definido pelo desenvolvedor da aplicação. O SgdpMaskService deve herdar a classe SgdpMaskService e deve ser registrado durante a etapa de registro de serviços (método AddSgdpMaskService<>()), juntamente com o serviço definido pelo usuário para tratamento de comandos de dados.

O método abstrato ExecuteAsync deve ser substituido (override) pelo desenvolvedor. O método recebe o contexto do comando de anonimização, contendo uma lista de identificadores de titular de dados. O desenvolvedor é responsável por buscar os dados a partir dos identificadores e anonimizá-los (apagá-los) através do método Mask(), oriundo da classe base e responsável também pela construção da resposta. 
A anonimização só deve ser persistida caso a flag ToVerify do comando seja verdadeira. Do contrário, será apenas enviada a resposta.
Por fim, o handler de mensagem subjacente do Tnf.Sgdp envia a resposta ao sgdp-service.
```c#
public class SampleSgdpMaskService : SgdpMaskService
{
    private readonly ICustomerRepository _customerRepository;

    public SampleSgdpMaskService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public override async Task ExecuteAsync(SgdpMaskCommandContext context, CancellationToken cancellationToken = default)
    {
        var hasCpf = context.Identifiers.TryGetValue("CPF", out var cpf);
        var hasEmail = context.Identifiers.TryGetValue("EMAIL", out var email);
        var hasRg = context.Identifiers.TryGetValue("RG", out var rg);

        if (hasCpf || hasEmail || hasRg)
        {
            var data = await _customerRepository.GetCustomerAsync(
                customer => (cpf.IsNullOrEmpty() || customer.Cpf.Equals(cpf))
                    && (email.IsNullOrEmpty() || customer.Email.Equals(email))
                    && (rg.IsNullOrEmpty() || customer.Rg.Equals(rg))
                );

            if (data != null)
            {
                Mask(data, context);

                if (!context.ToVerify)
                {
                    await _customerRepository.UpdateCustomerAsync(data);
                }
            }
        }

    }
}
```

#### SgdpMaskResponse

A mensagem de resposta enviada ao sgdp-service tem o formato abaixo:
```json
{
  "header": {
    "type": "SGDPMaskResponse",
    "tenantId": "b6572633-5a42-4d54-b2bf-8b06e4b92325",
    "generatedOn": "2020-12-07T13:03:50.259122-03:00",
    "locale": "en_GB"
  },
  "content": {
    "requestID": "fff9f4d2-a1e4-406c-8d78-e7978f7dc25b",
    "applicationId": "sgdp-metadata-applicationId",
    "exceptionAttributes": {
      "br.com.sgdp.model.Customer": {
        "description": "Cadastro de Clientes",
        "attributes": {
          "gender": {
            "type": "String",
            "description": "Gender",
            "sgdpData": {
              "sensitive": true,
              "type": "EMPTY",
              "allowsAnonymization": false,
              "identification": false
            },
            "sgdpPurposes": [],
            "reason": "Razão pela não anonimização"
          }
        }
      }
    }
  }
}
```
