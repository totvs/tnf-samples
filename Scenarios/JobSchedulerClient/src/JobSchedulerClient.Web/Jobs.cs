using System;
using System.Threading.Tasks;
using Tnf.Jobs;
using Tnf.Runtime.Session;

namespace JobScheduler.GuineaPig
{
    //Atributo TnfJobAuthorize é obrigatório e sera enviado para o RAC
    [TnfJobAuthorize("FailingJob", "Failing Job")]
    public class FailingJob : Job<EmptyParam>
    {
        public override Task ExecuteAsync(EmptyParam parameters)
        {
            //Quando a execução de um jobs joga uma exceção, esta execução é marcada como falha
            //Exceções que causam a falha de execução são enviadas de volta ao jobs scheduler 
            //e são inseridas como logs da execução
            throw new InvalidOperationException("Can't execute this job now.");
        }
    }
    
    [TnfJobAuthorize("MultipleStepsJob", "Job With Multiple Steps")]
    public class MultipleStepsJob : Job<EmptyParam>
    {
        public override async Task ExecuteAsync(EmptyParam parameters)
        {
            await Task.Delay(3000);

            //É possível informar o andamento do um job
            //Basta chamar esse método apos cadas etapa do job
            //O progresso do job é informado em forma de frações
            //Nessa caso estamos informando seguinte situação do job: 
            //passo 1 de 3 concluído
            await SetProgressAsync(1, 3);

            await Task.Delay(3000);

            await SetProgressAsync(2, 3);

            await Task.Delay(3000);

            await SetProgressAsync(3, 3);
        }
    }

    [TnfJobAuthorize("UnevenProgressJob", "Uneven Progress Job")]
    public class UnevenProgressJob : Job<EmptyParam>
    {
        public override async Task ExecuteAsync(EmptyParam parameters)
        {
            //O progresso de um job pode ser errático

            await Task.Delay(3000);

            await SetProgressAsync(1, 2);

            await Task.Delay(3000);

            await SetProgressAsync(2, 3);

            await Task.Delay(3000);
            
            //Nesta chamada não só voltamos o progresso do job, como também mudamos o fracionamento
            await SetProgressAsync(1, 5);

            await Task.Delay(3000);

            await SetProgressAsync(2, 5);

            await Task.Delay(3000);

            await SetProgressAsync(1, 2);

            await Task.Delay(3000);

            await SetProgressAsync(4, 5);

            await Task.Delay(3000);

            //Ao marcar o job como progresso total (1 de 10) não ira trocar o status para concluído
            //A informação de progresso apenas como feedback para a tela
            await SetProgressAsync(1, 1);

        }
    }

    [TnfJobAuthorize("LogExceptionJob", "Job That Will Log An Exception")]
    public class LogExceptionJob : Job<EmptyParam>
    {
        public override async Task ExecuteAsync(EmptyParam parameters)
        {
            try
            {
                throw new InvalidOperationException("Something went wrong!");
            }
            catch (Exception ex)
            {
                //Também é possível logar exceções que ocorreram durante a execução de um job, sem causar a falha do mesmo
                await LogExceptionAsync(ex);
            }
        }
    }

    //O atributo TnfJob permite dar um JobId diferente do nome da classe,
    //É uma boa pratica pois permite renomear a classe sem alterar o nome original do job
    //O atributo também recebe o parâmetro description, que é um nome amigável do job para ser usado no Job Scheduler
    [TnfJob("JobWithUserFriedlyName", "Job With User Friendly Name")]
    [TnfJobAuthorize("JobWithUserFriedlyName", "Job With User Friendly Name")]
    public class JobWithUserFriedlyName : Job<EmptyParam>
    {
        public override Task ExecuteAsync(EmptyParam parameters)
        {
            return Task.CompletedTask;
        }
    }

    [TnfJobAuthorize("LogSessionJob", "Log Session Job")]
    public class LogSessionJob : Job<EmptyParam>
    {
        private readonly ITnfSession _session;

        //Implementação de jobs também podem usar injeção de dependência assim como o resto da stack do TNF
        //Todas a interfaces e classes registrados do injetor de dependência da sua aplicação estarão disponível
        //para usar dentro das classes de job.
        public LogSessionJob(ITnfSession session)
        {
            _session = session;
        }

        public override async Task ExecuteAsync(EmptyParam parameters)
        {
            //É possível logar Texto puro também
            //Tudo que for logado através desse método irá parecer nos logs de execução.
            await LogTextAsync($"UserId was: {_session.UserId}");
            await LogTextAsync($"Tenant was: {_session.TenantId}");
        }
    }

    [TnfJobAuthorize("AllTypeOfParametersJob", "Job With All Types Of Parameters")]
    public class AllTypeOfParametersJob : Job<AllTypesParameters>
    {
        public override async Task ExecuteAsync(AllTypesParameters parameters)
        {
            //O TParameters da classe Job<TParameters> é uma class para transportar os parâmetros do job
            //Aqui temos um job que recebe todos os tipos de parâmetros aceitados
            //Parâmetros também podem ser nullable (Nullable<T>)
            await LogTextAsync($"{nameof(parameters.ShortParam          )} was: {parameters.ShortParam         }");
            await LogTextAsync($"{nameof(parameters.UshortParam         )} was: {parameters.UshortParam        }");
            await LogTextAsync($"{nameof(parameters.IntParam            )} was: {parameters.IntParam           }");
            await LogTextAsync($"{nameof(parameters.UintParam           )} was: {parameters.UintParam          }");
            await LogTextAsync($"{nameof(parameters.LongParam           )} was: {parameters.LongParam          }");
            await LogTextAsync($"{nameof(parameters.UlongParam          )} was: {parameters.UlongParam         }");
            await LogTextAsync($"{nameof(parameters.FloatParam          )} was: {parameters.FloatParam         }");
            await LogTextAsync($"{nameof(parameters.DoubleParam         )} was: {parameters.DoubleParam        }");
            await LogTextAsync($"{nameof(parameters.DecimalParam        )} was: {parameters.DecimalParam       }");
            await LogTextAsync($"{nameof(parameters.BoolParam           )} was: {parameters.BoolParam          }");
            await LogTextAsync($"{nameof(parameters.CharParam           )} was: {parameters.CharParam          }");
            await LogTextAsync($"{nameof(parameters.StringParam         )} was: {parameters.StringParam        }");
            await LogTextAsync($"{nameof(parameters.DateTimeParam       )} was: {parameters.DateTimeParam      }");
            await LogTextAsync($"{nameof(parameters.TimeSpanParam       )} was: {parameters.TimeSpanParam      }");
            await LogTextAsync($"{nameof(parameters.DateTimeOffsetParam )} was: {parameters.DateTimeOffsetParam}");
        }
    }

    [TnfJobAuthorize("JobParameterAttributeJob", "Job With JobParameterAttributes")]
    public class JobParameterAttributeJob : Job<RequiredAndLabel>
    {
        public override Task ExecuteAsync(RequiredAndLabel parameters)
        {
            return Task.CompletedTask;
        }
    }

    //Classes de parâmetros de jobs são classes com propriedades simples
    //Estas classes não devem conter propriedades de tipos complexos ou uma exceção será gerado ao iniciar a aplicação
    public class RequiredAndLabel
    {
        //Este parâmetro terá a label e a obrigatoriedade extraídos pelo nome e tipo da propriedade
        public int LabelExtractedFromProperty { get; set; }

        //È possível usar o atributo TnfJobParameter para informar um label para o parâmetro, assim como a obrigatoriedade desse parâmetro
        [TnfJobParameter("Label Extracted From Attribute", true)]
        public int ParamNameFromAttribute { get; set; }

        //Propriedade com tipos Nullable<T> serão marcas automaticamente como parâmetros não obrigatórios
        public int? IsRequiredExtractedFromPropertyType { get; set; }

        //Com o atributo TnfJobParameter é possível marcar um parâmetro como não obrigatório mesmo que a propriedade não seja Nullable<T>
        [TnfJobParameter("Is Required False Extracted From Attribute", false)]
        public int IsRequiredFalseFromAttribute { get; set; }
    }

    public class AllTypesParameters
    {
        public short ShortParam { get; set; }
        public ushort UshortParam { get; set; }
        public int IntParam { get; set; }
        public uint UintParam { get; set; }
        public long LongParam { get; set; }
        public ulong UlongParam { get; set; }
        public float FloatParam { get; set; }
        public double DoubleParam { get; set; }
        public decimal DecimalParam { get; set; }
        public bool BoolParam { get; set; }
        public char CharParam { get; set; }
        public string StringParam { get; set; }
        public DateTime DateTimeParam { get; set; }
        public TimeSpan TimeSpanParam { get; set; }
        public DateTimeOffset DateTimeOffsetParam { get; set; }
    }

    public class EmptyParam
    {
    }
}
