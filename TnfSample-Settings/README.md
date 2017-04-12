O TnfSample-Settings foi construído utilizando o TotvsNetFramework

Este é um exemplo de settings em arquivo/fixo podendo sobreescrever os valores buscando do banco

Pré-Requisitos

    Por definição da totvs não utilizaremos migration na nossa aplicação e portanto também não utilizaremos nos 
exemplos.
    É necessário criar criar um banco, execute o script ScriptDB-Settings.sql na pasta raiz deste projeto.
    Abra a solution e altere o arquivo ~\src\Tnf.Sample.Web\appsettings.json e insira os dados da sua conexão com o banco.
    A aplicação está pronta para rodar.

Funcionalidade

    A aplicação possui um cadastro simples de tarefas e o swagger que expõe as APIs para crud de settings

    Temos dois tipos de poviders de settings, são eles:

        Arquivo:
            Neste caso podemos utilizar a mesma estrutura de arquivo já suportada pelo dotnetcore, conforme exemplo:

                    {
                        "option1": "value1_from_json",
                        "option2": "value2_from_json",

                        "subsection": {
                            "suboption1": "subvalue1_from_json"
                        }
                    }

            Temos uma implementação desse arquivo em:  ~\src\Tnf.Sample.Web\SampleSetting.json.

            É necessário criar um provider que irá informar a estrutura de leitura desse arquivo(Tnf.App.Configuration.SettingsFileProvider).
                    * É obrigatório passar a lista de arquivo que deseja carregar no método GetJsonFiles()
                    * É possível sobreescrever a rotina GetRootSection, que por default busca o root do arquivo.

             Temos uma implementação dessa classe em: ~\src\Tnf.Sample.Web\Settings\AppSettingFileProvider.cs


      Compilado:
        Neste caso podemos implementar vários providers de settings.

            public class AppSettingsDbProvider : Tnf.App.Configuration.SettingProvider
            {
                public override IEnumerable<SettingDefinition> GetSettingDefinitions()
                {
                    return new List<SettingDefinition>
                        {
                            new SettingDefinition("Setting1", "A"),
                            new SettingDefinition("Setting2", "B")
                        };
                }
            }


     Para os dois modelos, é implementado um repositório de Setting(tabela: TnfSettings), que irá substituir todos os valores pelo encontrado no banco de dados.
     
     No Sample temos dois providers, um de arquivo e um copilado, são eles:

        Compilado: ~\Tnf.Sample.Web\Settings\AppSettingsDbProvider.cs
        Arquivo:   ~\Tnf.Sample.Web\Settings\AppSettingsFileProvider.cs

        Para que eles sejam adicionados na infraestrutura do TNF e necessário executar o comando abaixo no module da aplicação, que no caso do sample foi 
    feito em : ~\src\Tnf.Sample.Web\Startup\SampleWebModule.cs

            Configuration.Settings.Providers.Add<AppSettingsDbProvider>();
            Configuration.Settings.Providers.Add<AppSettingFileProvider>();


        A interface ISettingManager é utilizada para fazer a manipulação do conjunto de settings em tempo de runtime, basta utilizar os conceitos de IoC,
    o Tnf faz a exposição dos settings pelo swagger utilizando a mesma ideia, exemplo:
            public class SettingController
            {
                ISettingManager _manager;
                public SettingController(ISettingManager manager)
                {
                    _manager = manager;

                }

                [HttpGet]
                public async Task<IReadOnlyList<ISettingValue>> GetAllSettingValuesAsync()
                {           
                    return await _manager.GetAllSettingValuesAsync();
                }
            }
