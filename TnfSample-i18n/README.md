O TnfSample-i18n foi construído utilizando o TotvsNetFramework

Este é um exemplo de localização sendo ela multiTenant ou não.

Pré-Requisitos

    Por definição da totvs não utilizaremos migration na nossa aplicação e portanto também não utilizaremos nos 
exemplos.
    É necessário criar criar um banco, execute o script ScriptDB-i18n.sql na pasta raiz deste projeto.
    Abra a solution e altere o arquivo ~\src\Tnf.Sample.Web\appsettings.json e insera os dados da sua conexão com o banco.
    A aplicação está pronta para rodar.

Funcionalidade

    A aplicação possui um cadastro simples de tarefas e um seletor de idioma.

    Temos dois modelos de localização, são eles:

        Arquivo:
            No caso desse exemplo possuimos dois arquivos de localização(Inglês e Português)
                    ~\src\Tnf.Sample.Core\Localization\SourceFiles\SampleApp.json
                    ~\src\Tnf.Sample.Core\Localization\SourceFiles\SampleApp-pt-BR.json
            
            Para habilitar a localização e necessário adicionar na configuração do módulo, como está feito em : ~\src\Tnf.Sample.Core\SampleCoreModule.cs
    
            A partir desse momento qualquer localização irá levar em consideração a cultura da thread corrente para entregar a localização correta.

            Existe a interface ILocalizationSource que possui os mecanismos de localização, para os casos de Controller, ApplicationService e DomainService
          é possível chamar this.L(string name) onde o "name" é a key desejada, com isso o Tnf será responsável de entregar respectivo value.

        MultiTenant:

            No caso de multiTenant possuimos duas tabelas TnfLanguages e TnfLanguageTexts, as definições feitas para arquivo devem existir neste modelos
          também, pois caso o Tnf por algum motivo não encontre a localização no banco irá devolver a definida no arquivo(faultBack sempre é arquivo).
            Neste modelo é necessário que seja inserido dependência para o TnfAppEntityFrameworkCoreModule no módulo do EntityFramework, como feito
          no arquivo ~\src\Tnf.Sample.EntityFrameworkCore\EntityFrameworkCore\SampleEntityFrameworkCoreModule.cs
            Por fim é necessário habilitar a localização no banco como feito em ~\src\Tnf.Sample.Web\Startup\SampleWebModule.cs

                // Inicializa a localização multiTenant, sendo o faultBack as localiações em arquivo
                Configuration.Modules.TnfApp().LanguageManagement.EnableDbLocalization();

            É possível também utilizar localiações diferentes por tenant nesse caso irá utilizar a thread corrente para resgatar qual tenant deve ser 
         utilizado para filtrar.