using Tnf.SmartX.Domain.CodeFirst.Entities;

namespace Tnf.SmartX.Domain;

[SxRoutine(RoutineName = "CompanyRoutine", Title = "CompanyRoutine", Version = "1.0")]
public class CompanyRoutineUI : CompanyEntity, ISXRoutineLayoutDataOperations
{
    public void ConfigureLayout(IRoutineLayoutDataOperationsBuilder builder)
    {
        builder
            .AddDataView(x => SetupDataView(x))
            .AddDataNew(x => SetupView(x, "dataNewCompany", "Nova companhia"))
            .AddDataDetail(x => SetupView(x, "dataDetailCompany", "Companhia"))
            .AddDataEdit(x => SetupView(x, "dataEditCompany", "Editar Companhia"));

        //AddEvents(builder);
    }

    private void AddEvents(IRoutineLayoutDataOperationsBuilder builder)
    {
        builder.AddEvents(e => e
            .AddOnBlur(b => b
                .WithContext([RoutineLayoutEnum.DataNew, RoutineLayoutEnum.DataEdit])
                    .WithFields([nameof(RegistrationNumber)])
                .AddAction(a => a
                    .AddServerValidateAction(sv => sv
                        .WithIdentifier("validateRegistrationNumber")
                        //.GetPropertyValue(nameof(RegistrationNumber))

                        )
                    )
                )
            );

    }

    private static void SetupDataView(IRoutineViewDataViewBuilder builder)
    {

        builder
            .WithIdentifier("dataViewCompanies")
            .WithTitle("Listagem de empresas")
            .WithIndex(true)
            .AddTable(e => e
                .WithIdentifier("tableCompanies")
                .SetColumn(c => c.WithProperty(nameof(TenantId)))
                .SetColumn(c => c.WithProperty(nameof(Name)))
                .SetColumn(c => c.WithProperty(nameof(TradeName)))
                .SetColumn(c => c.WithProperty(nameof(RegistrationNumber)))
                .SetColumn(c => c.WithProperty(nameof(Email)))
            )
            .Configuration(c => c
                .AddTableAction(ta => ta.WithMinSelectedItems(2).WithMaxSelectedItems(5)
                  .AddAction(a => a
                    .AddShowMessageAction(sm => sm
                        .WithIdentifier("teste")
                        .WithLabel("Teste")
                        .WithMessage("OK!")
                    )
                  )
                )
                .AddPageAction(pa => pa
                  .AddAction(a => a
                    .AddNavigateAction(n => n
                        .WithIdentifier("navigateDocHelp")
                        .WithLabel("Ajuda")
                        .WithTarget(ActionTargetEnum.Blank)
                        .WithUrl("https://tnf.totvs.com.br/home"))
                    )
                  )
                .AddPageAction(pa => pa
                    .AddAction(a => a
                        .AddNavigateAction(n => n
                            .WithIdentifier("incluirCompanhia")
                            .WithLabel("Ação de Página Incluir")
                            .WithTargetView("dataNewCompany")
                        )
                    )
                )
            //.AddPageAction(pa => pa
            //    .AddAction(a => a
            //        .AddRoutineAction(ra => ra
            //            .WithIdentifier("routineActionTeste")
            //            .WithLabel("Editar companhia")
            //            .WithRoutine("CompanyRoutine")
            //            .WithTargetView("dataEditCompany/{{$model.id}}")
            //        ))
            //    )
            )
            .SetProperty(p => p
                .WithProperty(nameof(Id))
                .WithHidden(true)
             )
            .SetProperty(p => p
                .WithProperty(nameof(TenantId))
                .WithHidden(true)
            )
            .SetProperty(p => p
                .WithProperty(nameof(RegistrationNumber))
                .WithMask("99.999.999/9999-99")
            );

    }

    private static void SetupView<T>(
        IRoutineViewBaseBuilder<T> builder,
        string identifier,
        string title) where T : class
    {
        builder.WithIdentifier(identifier);
        builder.WithTitle(title);
        builder.AddSection(s => s
            .WithIdentifier("sectionCompany")
            .WithTitle("Dados da companhia")
            .AddElement(e => e.WithProperty(nameof(Email)))
            .AddElement(e => e.WithProperty(nameof(Name)))
            .AddElement(e => e.WithProperty(nameof(TradeName)))
            .AddElement(e => e
                .WithProperty(nameof(RegistrationNumber))
                .WithMask("99.999.999/9999-99"))
            .AddElement(e => e.WithProperty(nameof(HasEsg)))
        );
        builder.SetProperty(p => p.WithProperty(nameof(Id)).WithHidden(true));
        builder.SetProperty(p => p.WithProperty(nameof(TenantId)).WithHidden(true));
        builder.SetProperty(p => p.WithProperty(nameof(LastModificationTime)).WithHidden(true));
        builder.SetProperty(p => p.WithProperty(nameof(CreationTime)).WithHidden(true));

        builder.SetProperty(p => p
            .WithProperty(nameof(RegistrationNumber))
            .WithPattern("^\\d{14}$")
            .WithRequired(true)
        );
    }

    private static IRoutineViewBaseBuilder<T> ApplyConfiguration<T>(IRoutineViewBaseBuilder<T> builder) where T : class
    {
        switch (builder)
        {
            case IRoutineViewDataDetailBuilder detailBuilder:
                detailBuilder.Configuration(ConfigureExclude);
                break;

            case IRoutineViewDataNewBuilder newBuilder:
                newBuilder.Configuration(ConfigureExclude);
                break;

            case IRoutineViewDataEditBuilder editBuilder:
                editBuilder.Configuration(ConfigureExclude);
                break;
        }

        return builder;
    }

    private static void ConfigureExclude<T>(IRoutineConfigBaseBuilder<T> builder) where T : class
    {
        var excludedProperties = new[] { nameof(Id), nameof(LastModificationTime), nameof(CreationTime) };

        foreach (var prop in excludedProperties)
        {
            builder.ExcludeProperty(p => p.WithProperty(prop));
        }
    }

    private static void ConfigureExclude<T>(IRoutineConfigBaseBuilder<T> builder, string[] excludedProperties) where T : class
    {
        foreach (var prop in excludedProperties)
        {
            builder.ExcludeProperty(p => p.WithProperty(prop));
        }
    }
}
