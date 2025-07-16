using Tnf.SmartX.Domain.DatabaseFirst.Usuario.Models;

namespace Tnf.SmartX.Domain.DatabaseFirst.Usuario.Routines;

[SxRoutine(RoutineName = "GlbUsuarioRoutineDataView", Title = "UserRoutineDataView", Version = "1.0")]
public class UsuarioRoutineUIDataView : UsuarioModelEntity, ISXRoutineLayoutDataView
{
    public void ConfigureLayout(IRoutineLayoutDataViewBuilder builder)
    {
        builder
            .AddEvents(e => e
                .AddOnLoad(l => l.WithContext([RoutineLayoutEnum.DataDetail])
                    .AddAction(a => a
                        .AddShowMessageAction(s => s.WithIdentifier("showMessageOnLoadEvent").WithLabel("TESTE AÇÃO!")
                            .WithMessage("Evento [OnLoad] disparado com sucesso!").WithMessageType(ActionMessageTypeEnum.Info))))
                )
            .AddDataView(x => x.WithIdentifier("dataViewUsers").WithTitle("Listagem de Usuários").WithIndex(true)
                .AddTable(e => e.WithIdentifier("tableUsers")
                    .SetColumn(c => c.WithProperty(nameof(Usuario)))
                    .SetColumn(c => c.WithProperty(nameof(Nome)))
                    .SetColumn(c => c.WithProperty(nameof(Email)))
                    .SetColumn(c => c.WithProperty(nameof(DataInicio)))
                    .SetColumn(c => c.WithProperty(nameof(CodAcesso)))
                    // .SetColumn(c => c.WithProperty(nameof(DescricaoAcesso)))
                    // .SetColumn(c => c.WithProperty(nameof(Apelido)))
                    // .SetColumn(c => c.WithProperty(nameof(Chapa)))
                    .SetFilter(f => f.WithProperty(nameof(Usuario)).WithBasicOperator(FilterOperatorEnum.Equal, false))
                    .SetFilter(f =>
                        f.WithProperty(nameof(Nome))
                            .WithAdvancedOperator([FilterOperatorEnum.Equal, FilterOperatorEnum.Contains],
                                false)))
            );
    }
}
