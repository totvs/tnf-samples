using Tnf.SmartX.Domain.CodeFirst.Entities;

namespace Tnf.SmartX.Domain;

[SxRoutine(RoutineName = "CustomerRoutine", Title = "CustomerRoutine", Version = "1.0")]
public class CustomerRoutineUI : CustomerEntity, ISXRoutineLayoutDataOperations
{
    public void ConfigureLayout(IRoutineLayoutDataOperationsBuilder builder)
    {
        SetupDataView(builder);
        SetupDataNew(builder);
        SetupDataEdit(builder);
        SetupDataDetail(builder);
    }

    private static void SetupDataEdit(IRoutineLayoutDataOperationsBuilder builder)
    {
        builder.AddDataEdit(d => d
            .WithIdentifier("dataEditCustomer")
            .WithTitle("Atualizar entrega")
            .WithElementsBase("dataNewCustomer")
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
                .WithProperty(nameof(Addresses))
                .SetChildProperty(cp => cp
                    .WithProperty(nameof(AddressEntity.ZipCode))
                    .WithMask("99999-999")
                )
            )
        );

        builder.SetProperty(p => p
            .WithProperty("addresses")
            .SetChildProperty(cp => cp
                .WithProperty("state")
                .AddLookup(l => l
                    .WithModelRef("StateEntityModel")
                    .WithDisplayFields(["uf", "description"])
                    .WithFieldValue("uf")
                    .WithMultiSelect(false)
                )
            )
        );
    }

    private static void SetupDataDetail(IRoutineLayoutDataOperationsBuilder builder)
    {
        builder.AddDataDetail(d => d
            .WithIdentifier("dataDetailCustomer")
            .WithElementsBase("dataNewCustomer")
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
                .WithProperty(nameof(Addresses))
                .SetChildProperty(cp => cp
                    .WithProperty(nameof(AddressEntity.ZipCode))
                    .WithMask("99999-999")
                )
            )
        );
    }

    private static void SetupDataNew(IRoutineLayoutDataOperationsBuilder builder)
    {
        builder.AddDataNew(d => d
            .WithIdentifier("dataNewCustomer")
            .WithTitle("Nova entrega")
            .AddSection(s => s
                .WithIdentifier("customerSection")
                .WithTitle("Dados do entregador")
                .AddElement(e => e
                    .WithProperty(nameof(Name)))
                .AddElement(e => e
                    .WithProperty(nameof(Email)))
                .AddElement(e => e
                    .WithProperty(nameof(PhoneNumber)))
            )
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
                .WithProperty(nameof(TenantId))
                .WithHidden(true)
             )
            .SetProperty(p => p
                .WithProperty(nameof(Addresses))
                .SetChildProperty(cp => cp
                    .WithProperty(nameof(AddressEntity.Deliveries))
                    .SetChildProperty(cp => cp
                        .WithProperty(nameof(DeliveryEntity.ScheduledDate))
                        .WithDefaultValue(DateTime.UtcNow.ToString("yyyy-MM-dd"))
                    )
                )
            )
            .SetProperty(p => p
                .WithProperty(nameof(Addresses))
                .SetChildProperty(cp => cp
                    .WithProperty(nameof(AddressEntity.ZipCode))
                    .WithMask("99999-999")
                )
            )
        );

        builder.SetProperty(p => p
            .WithProperty("addresses")
            .SetChildProperty(cp => cp
                .WithProperty("state")
                .AddLookup(l => l
                    .WithModelRef("StateEntityModel")
                    .WithDisplayFields(["uf", "description"])
                    .WithFieldValue("uf")
                    .WithMultiSelect(false)
                )
            )
        );
    }

    private static void SetupDataView(IRoutineLayoutDataOperationsBuilder builder)
    {
        builder.AddDataView(d => d
            .WithIdentifier("dataViewCustomers")
            .WithTitle("Listagem de entregadores")
            .AddTable(c => c
                .WithIdentifier("tableCustomers")
                .SetColumn(c => c.WithProperty(nameof(Name)))
                .SetColumn(c => c.WithProperty(nameof(Email)))
                .SetColumn(c => c.WithProperty(nameof(PhoneNumber)))
                )
            )
            .SetProperty(p => p
                .WithProperty(nameof(Name))
                .WithTitle("Nome")
            )
            .SetProperty(p => p
                .WithProperty(nameof(Email))
                .WithTitle("E-mail")
            )
            .SetProperty(p => p
                .WithProperty(nameof(PhoneNumber))
                .WithTitle("Telefone")
                .WithPattern("^(\\(\\d{2}\\)\\s9\\d{4}-\\d{4}|\\d{2}9\\d{8})$")
                .WithMask("(99) 99999-9999")
            );


    }    
}
