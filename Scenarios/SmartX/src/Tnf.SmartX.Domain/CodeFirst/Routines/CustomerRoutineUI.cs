using Tnf.SmartX.Domain.CodeFirst.Entities;

namespace Tnf.SmartX.Domain;

[SxRoutine(RoutineName = "CustomerRoutine", Title = "CustomerRoutine", Version = "1.0")]
public class CustomerRoutineUI : CustomerEntity, ISXRoutineLayoutDataOperations
{
    public void ConfigureLayout(IRoutineLayoutDataOperationsBuilder builder)
    {
        SetProperties(builder);
        SetupEvents(builder);
        SetupDataView(builder);
        SetupDataNew(builder);
        SetupDataEdit(builder);
        SetupDataDetail(builder);
    }

    private void SetProperties(IRoutineLayoutDataOperationsBuilder builder)
    {
        builder
            .SetProperty(p => p
                .WithProperty(nameof(Name))
                .WithAutoinc(false)
                .WithBooleanLabelFalse("Boolean Label False")
                .WithBooleanLabelTrue("Boolean Label True")
                .WithDefaultValue("DefaultValue")
                .WithDescription("Description")
                .WithFormat("email")
                .WithHelp("Help")
                .WithHidden(false)
                .WithLabel("Label")
                .WithMaskFormatModel(false)
                .WithMaximum(5)
                .WithMaxLength(50)
                .WithMinimum(1)
                .WithMinLength(1)
                .WithMultivalue(false)
                .WithOrigin("custom")
                .WithReadOnly(false)
                .WithRequired(true)
                .WithType(typeof(string))
                .WithUpperCase(true)
                .WithTitle("Nome")
                .AddGridSystem(gd => gd
                    .WithExtraLargeColumns(6)
                    .WithLargeColumns(6)
                    .WithMediumColumns(12)
                    .WithSmallColumns(12)
                )
            )
            .SetProperty(p => p
                .WithProperty(nameof(LastName))
                .WithTitle("Sobrenome")
                .AddGridSystem(gd => gd
                    .WithExtraLargeColumns(6)
                    .WithLargeColumns(6)
                    .WithMediumColumns(12)
                    .WithSmallColumns(12)
                )
            )
            .SetProperty(p => p
                .WithProperty(nameof(Email))
                .AddComponent(c => c
                        .AddInputEmail(ie => ie
                            .WithPlaceholder("E-mail")
                        )
                    )
                .WithTitle("E-mail")
                .AddGridSystem(gd => gd
                    .WithExtraLargeColumns(6)
                    .WithLargeColumns(6)
                    .WithMediumColumns(6)
                    .WithSmallColumns(12)
                )
            )
            .SetProperty(p => p
                .WithProperty(nameof(PhoneNumber))
                .WithTitle("Telefone")
                .WithPattern("^(\\(\\d{2}\\)\\s9\\d{4}-\\d{4}|\\d{2}9\\d{8})$")
                .WithMask("(99) 99999-9999")
                .AddGridSystem(gd => gd
                    .WithExtraLargeColumns(6)
                    .WithLargeColumns(6)
                    .WithMediumColumns(6)
                    .WithSmallColumns(12)
                )
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
                    .WithProperty(nameof(AddressEntity.ZipCode))
                    .WithOrder(1)
                    .WithMask("99999-999")
                    .AddLookup(l => l
                        .WithModelRef("ZipCodeEntityModel")
                        .WithDisplayFields(["zipCode", "street", "city", "state"])
                        .WithFieldValue("zipCode")
                        .WithMultiSelect(false)
                        )
                    )
                .SetChildProperty(cp => cp
                    .WithProperty(nameof(AddressEntity.Street))
                    .WithOrder(2)
                    )
                .SetChildProperty(cp => cp
                    .WithProperty(nameof(AddressEntity.City))
                    .WithOrder(3)
                    )
                .SetChildProperty(cp => cp
                    .WithProperty(nameof(AddressEntity.State))
                    .WithOrder(4)
                    )
                .SetChildProperty(cp => cp
                    .WithProperty(nameof(AddressEntity.Deliveries))
                    .SetChildProperty(cp => cp
                    .WithProperty(nameof(DeliveryEntity.ScheduledDate))
                    .WithDefaultValue(DateTime.UtcNow.ToString("yyyy-MM-dd"))
                    )
                )
            )
            .SetProperty(p => p
                .WithProperty(nameof(ZipCodeEntity.ZipCode))
                .WithMask("99999-999")
            );
    }

    private static void SetupEvents(IRoutineLayoutDataOperationsBuilder builder)
    {
        builder.AddEvents(e => e
            .AddOnChange(b => b
                .WithContext([RoutineLayoutEnum.DataNew, RoutineLayoutEnum.DataEdit])
                //.WithFields([nameof(Name)])
                //.WithFields([$"{nameof(Addresses)}.{nameof(AddressEntity.ZipCode)}"])
                // .WithFields(
                //     (CustomerEntity c) => c.Addresses.First().ZipCode
                // )
                //.WithFields( f => f
                //    .AddField(x => x.WithNestedProperty(nameof(Addresses), nameof(AddressEntity.ZipCode)))
                //)

                .AddAction(a => a
                    .AddApiCallAction(api => api
                        .WithIdentifier("apiCallGetAddress")
                        .WithMethod(ActionMethodEnum.GET)
                        .WithEndpoint("data/ZipCodeEntityModel/{{$model.addresses.zipCode}}")
                        .AddHeaders(h => h.WithHeader("Content-Type", "application/json"))
                        .AddActionSuccess(acs => acs
                        .AddSetFieldsAction(fa => fa
                            .WithIdentifier("setFields")
                            .AddField(f => f
                                .SetProperty(p => p.WithNestedProperty(nameof(Addresses), nameof(AddressEntity.Street)))
                                //.SetProperty(p => p.WithNestedProperty(nameof(Addresses), nameof(AddressEntity.Street)))
                                .WithValue("{{$response.street}}")
                            )
                            .AddField(f => f
                                .SetProperty(p => p.WithNestedProperty(nameof(Addresses), nameof(AddressEntity.City)))
                                .WithValue("{{$response.city}}")
                            )
                            .AddField(f => f
                                .SetProperty(p => p.WithNestedProperty(nameof(Addresses), nameof(AddressEntity.State)))
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
            //.AddOnBlur(c => c
            //    .WithContext([RoutineLayoutEnum.DataNew, RoutineLayoutEnum.DataEdit])
            //    .WithFields([nameof(Name)])
            //    .AddAction(a => a
            //        .AddShowMessageAction(sm => sm
            //            .WithMessageType(ActionMessageTypeEnum.Success)
            //            .WithMessage("Tá gostando.")
            //        )
            //    )
            //)
            //.AddOnChangeModel(cm => cm
            //    .WithContext([RoutineLayoutEnum.DataNew, RoutineLayoutEnum.DataEdit])
            //    .AddAction(a => a
            //        .AddShowMessageAction(ma => ma
            //            .WithMessageType(ActionMessageTypeEnum.Success)
            //            .WithMessage("Action AddOnChangeModel")
            //        )
            //    )
            //)
            .AddOnActionEditEditableGrid(eg => eg
                .WithContext([RoutineLayoutEnum.DataNew, RoutineLayoutEnum.DataEdit])
                //.WithFields(f => f
                //    .AddField(ff => ff.WithNestedProperty(nameof(Addresses), nameof(AddressEntity.State)))
                //    .AddField(ff => ff.WithNestedProperty(nameof(Addresses), nameof(AddressEntity.City)))
                //    .AddField(ff => ff.WithNestedProperty(nameof(Addresses), nameof(AddressEntity.ZipCode)))
                //    .AddField(ff => ff.WithNestedProperty(nameof(Addresses), nameof(AddressEntity.Street)))
                //)
                .AddAction(a => a
                    .AddShowMessageAction(ma => ma
                        .WithMessageType(ActionMessageTypeEnum.Info)
                        .WithMessage("Action AddOnActionEditEditableGrid")
                    )
                )
            )
            .AddOnAfterSaveEditableGrid(eg => eg
                .WithContext([RoutineLayoutEnum.DataNew, RoutineLayoutEnum.DataEdit])
                .AddAction(a => a
                    .AddShowMessageAction(ma => ma
                        .WithMessageType(ActionMessageTypeEnum.Info)
                        .WithMessage("Action AddOnAfterSaveEditableGrid")
                    )
                )
            )
            //.AddOnBeforeInsertEditableGrid(eg => eg
            //    .WithContext([RoutineLayoutEnum.DataNew, RoutineLayoutEnum.DataEdit])
            //    .AddAction(a => a
            //        .AddShowMessageAction(ma => ma
            //            .WithMessageType(ActionMessageTypeEnum.Info)
            //            .WithMessage("Action AddOnBeforeInsertEditableGrid")
            //        )
            //    )
            //)
            //.AddOnBeforeRemoveEditableGrid(eg => eg
            //    .WithContext([RoutineLayoutEnum.DataNew, RoutineLayoutEnum.DataEdit])
            //    .AddAction(a => a
            //        .AddShowMessageAction(ma => ma
            //            .WithMessageType(ActionMessageTypeEnum.Info)
            //            .WithMessage("Action AddOnBeforeRemoveEditableGrid")
            //        )
            //    )
            //)
            //.AddOnAfterRemoveEditableGrid(eg => eg
            //    .WithContext([RoutineLayoutEnum.DataNew, RoutineLayoutEnum.DataEdit])
            //    .AddAction(a => a
            //        .AddShowMessageAction(ma => ma
            //            .WithMessageType(ActionMessageTypeEnum.Info)
            //            .WithMessage("Action AddOnAfterRemoveEditableGrid")
            //        )
            //    )
            //)
            //.AddOnBeforeUndoRemoveEditableGrid(eg => eg
            //    .WithContext([RoutineLayoutEnum.DataNew, RoutineLayoutEnum.DataEdit])
            //    .AddAction(a => a
            //        .AddShowMessageAction(ma => ma
            //            .WithMessageType(ActionMessageTypeEnum.Info)
            //            .WithMessage("Action AddOnBeforeUndoRemoveEditableGrid")
            //        )
            //    )
            //)
            //.AddOnAfterUndoRemoveEditableGrid(eg => eg
            //    .WithContext([RoutineLayoutEnum.DataNew, RoutineLayoutEnum.DataEdit])
            //    .AddAction(a => a
            //        .AddShowMessageAction(ma => ma
            //            .WithMessageType(ActionMessageTypeEnum.Info)
            //            .WithMessage("Action AddOnAfterUndoRemoveEditableGrid")
            //        )
            //    )
            //)
            .AddBeforeSaveForm(sf => sf
                .WithContext([RoutineLayoutEnum.DataNew, RoutineLayoutEnum.DataEdit])
                .AddAction(a => a
                    .AddShowMessageAction(ma => ma
                        .WithMessageType(ActionMessageTypeEnum.Info)
                        .WithMessage("Action AddBeforeSaveForm")
                    )
                )
            )

        );
    }

    private static void SetupDataEdit(IRoutineLayoutDataOperationsBuilder builder)
    {
        builder.AddDataEdit(d => d
            .WithIdentifier("dataEditCustomer")
            .WithTitle("Atualizar entrega")
            .WithElementsBase("dataNewCustomer")
        );
    }

    private static void SetupDataDetail(IRoutineLayoutDataOperationsBuilder builder)
    {
        builder.AddDataDetail(d => d
            .WithIdentifier("dataDetailCustomer")
            .WithTitle("Detalhes da entrega")
            .WithElementsBase("dataNewCustomer")
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
                .AddElement(e => e.WithProperty(nameof(Name)))
                .AddElement(e => e.WithProperty(nameof(LastName)))
                .AddElement(e => e.WithProperty(nameof(Email)))
                .AddElement(e => e.WithProperty(nameof(PhoneNumber)))
            )
            //.AddButton(ab => ab
            //    .WithIdentifier("openModal")
            //    .WithLabel("Abril modal")
            //    .AddAction(a => a
            //        .AddInvokeMethodAction(ima => ima
            //            .WithIdentifier("openModalAction")
            //            .WithMethod(InvokeMethodEnum.Open)
            //            .WithTarget("testModal")
            //        )
            //    )
            //)
            .AddButton(ab => ab
                .WithIdentifier("openAddressRoutine")
                .WithLabel("Abrir AddressRoutine")
                .AddAction(a => a
                    .AddNavigateAction(ima => ima
                        .WithIdentifier("addressRoutineAction")
                        .WithLabel("Abrir AddressRoutine")
                        .WithUrl("smart-x/AddressRoutine")
                        .WithTarget(ActionTargetEnum.Self)
                    //.WithRoutine("CompanyRoutine")
                    //.WithTargetView("dataViewAddress")

                    // .AddRoutineAction(ima => ima
                    //     .WithIdentifier("addressRoutineAction")
                    //     .WithLabel("Abrir AddressRoutine")
                    //     .WithRoutine("CompanyRoutine")
                    //     .WithTargetView("dataViewAddress")
                    )
                )
            )
            //.AddButton(ab => ab
            //    .WithIdentifier("openPageSlider")
            //    .WithLabel("Abril page slide")
            //    .AddAction(a => a
            //        .AddInvokeMethodAction(ima => ima
            //            .WithIdentifier("openPageSlideAction")
            //            .WithMethod(InvokeMethodEnum.Open)
            //            .WithTarget("testPageSlide")
            //        )
            //    )
            //)
            //.AddModal(m => m
            //    .WithIdentifier("testModal")
            //    .WithTitle("Nova entrega")
            //    .AddSection(s => s
            //        .WithIdentifier("customerModalSection")
            //        .WithTitle("Dados do entregador")
            //        .AddElement(e => e
            //            .WithProperty(nameof(Name)))
            //        .AddElement(e => e
            //            .WithProperty(nameof(LastName)))
            //        .AddElement(e => e
            //            .WithProperty(nameof(Email)))
            //        .AddElement(e => e
            //            .WithProperty(nameof(PhoneNumber)))
            //    )
            //    .AddPrimaryAction(pa => pa
            //        .AddInvokeMethodAction(ma => ma
            //            .WithIdentifier("saveModal")
            //            .WithLabel("Salvar")
            //            .WithMethod(InvokeMethodEnum.Close)
            //            .WithTarget("testModal")
            //        )
            //    )
            //    .AddSecondaryAction(sa => sa
            //        .AddInvokeMethodAction(ma => ma
            //            .WithIdentifier("closeModal")
            //            .WithLabel("Fechar")
            //            .WithMethod(InvokeMethodEnum.Close)
            //            .WithTarget("testModal")
            //        )
            //    )
            //)
            .AddPageSlide(ps => ps
                .WithIdentifier("testPageSlide")
                .WithSubTitle("Page slide test")
                .AddText(t => t
                    .WithValue("Page slide test")
                )
            )
            .Configuration(c => c
                .AddPageAction(pa => pa
                    .AddAction(a => a
                        .AddInvokeMethodAction(ima => ima
                            .WithIdentifier("openPageSlideAction")
                            .WithLabel("Abrir page slide")
                            .WithMethod(InvokeMethodEnum.Open)
                            .WithTarget("testPageSlide")
                        )
                    )
                )
                .AddPageAction(pa => pa
                    .AddAction(a => a
                        //.AddRoutineAction(ra => ra
                        //    .WithLabel("Routine Action")
                        //    .WithIdentifier("routineAction")
                        //    .WithRoutine("AddressRoutine")
                        //    .WithTargetView("dataViewAddress")
                        //)
                        .AddInvokeMethodAction(ima => ima
                            .WithIdentifier("openModalAction")
                            .WithLabel("Abrir modal")
                            .WithMethod(InvokeMethodEnum.Open)
                            .WithTarget("testModal")
                        )
                    )
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
                .SetColumn(c => c.WithProperty(nameof(LastName)))
                .SetColumn(c => c.WithProperty(nameof(Email)))
                .SetColumn(c => c.WithProperty(nameof(PhoneNumber)))
            )
        );
    }
}
