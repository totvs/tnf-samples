using Tnf.SmartX.Domain.CodeFirst.Entities;

namespace Tnf.SmartX.Domain.CodeFirst.Routines;

[SxRoutine(RoutineName = "AddressRoutine", Title = "AddressRoutine", Version = "1.0")]
public class AddressRoutineUI : AddressEntity, ISXRoutineLayoutDataOperations
{
    public void ConfigureLayout(IRoutineLayoutDataOperationsBuilder builder)
    {
        SetProperties(builder);
        SetupEvents(builder);
        SetupDataView(builder);
        SetupDataNew(builder);
        //SetupDataEdit(builder);
        SetupDataDetail(builder);
    }

    private void SetProperties(IRoutineLayoutDataOperationsBuilder builder)
    {
        builder
            .SetProperty(p => p
                .WithProperty(nameof(Id))
                .WithHidden(true)
            )
            .SetProperty(p => p
                .WithProperty(nameof(CreationTime))
                .WithHidden(true)
            )
            .SetProperty(p => p
                .WithProperty(nameof(LastModificationTime))
                .WithHidden(true)
            )
            .SetProperty(p => p
                .WithProperty(nameof(ZipCode))
                .WithOrder(1)
                .WithMask("99999-999")
                .AddLookup(l => l
                    .WithModelRef("ZipCodeEntityModel")
                    .WithDisplayFields(["zipCode"])
                    .WithFieldValue("zipCode")
                    .WithMultiSelect(false)
                )
            )
            .SetProperty(cp => cp
                .WithProperty(nameof(Street))
                .WithOrder(2)
            )
            .SetProperty(cp => cp
                .WithProperty(nameof(City))
                .WithOrder(3)
            )
            .SetProperty(cp => cp
                .WithProperty(nameof(State))
                .WithOrder(4)
            );
    }

    private static void SetupEvents(IRoutineLayoutDataOperationsBuilder builder)
    {
        builder.AddEvents(e => e
            .AddOnChange(b => b
                .WithContext([RoutineLayoutEnum.DataNew, RoutineLayoutEnum.DataEdit])
                .WithFields(f => f.AddField(nameof(ZipCode)))
                .AddAction(a => a
                    .AddApiCallAction(api => api
                        .WithIdentifier("apiCallGetAddress")
                        .WithMethod(ActionMethodEnum.GET)
                        .WithEndpoint("data/ZipCodeEntityModel/{{$model.zipCode}}")
                        .AddHeaders(h => h.WithHeader("Content-Type", "application/json"))
                        .AddActionSuccess(acs => acs
                            .AddSetFieldsAction(fa => fa
                                .WithIdentifier("setFields")
                                .AddField(f => f
                                    .SetProperty(p => p.WithProperty(nameof(Street)))
                                    .WithValue("{{$response.street}}")
                                )
                                .AddField(f => f
                                    .SetProperty(p => p.WithProperty(nameof(City)))
                                    .WithValue("{{$response.city}}")
                                )
                                .AddField(f => f
                                    .SetProperty(p => p.WithProperty(nameof(State)))
                                    .WithValue("{{$response.state}}")
                                )
                            )
                        )
                        .AddActionError(err => err
                            .AddShowMessageAction(sm => sm
                                .WithMessage("Ocorreu um erro ao buscar o CEP.")
                                .WithMessageType(ActionMessageTypeEnum.Error)
                            )
                        )
                    )
                )
            )
        );
    }

    private static void SetupDataView(IRoutineLayoutDataOperationsBuilder builder)
    {
        builder.AddDataView(d => d
            .WithIdentifier("dataViewAddress")
            .WithTitle("Listagem de endereços")
            .AddTable(c => c
                .WithIdentifier("tableAddresses")
                .SetColumn(c => c.WithProperty(nameof(ZipCode)))
                .SetColumn(c => c.WithProperty(nameof(Street)))
                .SetColumn(c => c.WithProperty(nameof(City)))
                .SetColumn(c => c.WithProperty(nameof(State)))
            )
        );
    }

    private static void SetupDataNew(IRoutineLayoutDataOperationsBuilder builder)
    {
        builder.AddDataNew(d => d
            .WithIdentifier("dataNewAddress")
            .WithTitle("Novo endereço")
            .AddSection(s => s
                .WithIdentifier("addressSection")
                .WithTitle("Dados do endereço")
                .AddElement(e => e
                    .WithProperty(nameof(ZipCode)))
                .AddElement(e => e
                    .WithProperty(nameof(Street)))
                .AddElement(e => e
                    .WithProperty(nameof(City)))
                .AddElement(e => e
                    .WithProperty(nameof(State)))
            )
        );
    }

    private static void SetupDataDetail(IRoutineLayoutDataOperationsBuilder builder)
    {
        builder.AddDataDetail(d => d
            .WithIdentifier("dataDetailAddress")
            .WithTitle("Detalhes do endereço")
            .WithElementsBase("dataNewAddress")
        );
    }
}
