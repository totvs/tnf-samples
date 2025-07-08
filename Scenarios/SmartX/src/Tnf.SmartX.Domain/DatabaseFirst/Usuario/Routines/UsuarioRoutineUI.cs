using Tnf.SmartX.Domain.DatabaseFirst.Usuario.Models;

namespace Tnf.SmartX.Domain.DatabaseFirst.Usuario.Routines;

[SxRoutine(RoutineName = "GlbUsuarioRoutine", Title = "UserRoutine", Version = "1.0")]
public class UsuarioRoutineUI : UsuarioModelEntity, ISXRoutineLayoutDataOperations
{
    public void ConfigureLayout(IRoutineLayoutDataOperationsBuilder builder)
    {
        builder
            .AddEvents(e => e
                //.AddOnLoad(l => l.WithContext([RoutineLayoutEnum.DataView])
                //    .AddAction(a => a
                //        .AddShowMessageAction(s => s.WithIdentifier("showMessageOnLoadEvent").WithLabel("TESTE AÇÃO!")
                //            .WithMessage("Evento [OnLoad] disparado com sucesso!").WithMessageType(ActionMessageTypeEnum.Info))))
                .AddOnBlur(b => b.WithContext([RoutineLayoutEnum.DataNew, RoutineLayoutEnum.DataEdit]).WithFields([
                        nameof(Usuario)
                    ])
                    .AddAction(a => a
                      .AddApiCallAction(ac => ac.WithIdentifier("apiCallVerifyUser").WithMethod(ActionMethodEnum.GET).WithEndpoint("data/GlbUsuarioModel/U099").WithLabel("Ação")
                        .AddActionSuccess(acs => acs
                          .AddSetFieldsAction(sf => sf.WithIdentifier("setFieldsEmail").WithLabel("Ação")
                            .AddField(f => f
                              .SetProperty(p => p.WithProperty(nameof(Email)).WithReadOnly(true))
                              .WithValue("{{$response.email}}"))))
                        .AddActionError(ace => ace
                          .AddShowMessageAction(sm => sm.WithIdentifier("smEmailError").WithLabel("Ação")
                            .WithMessage("Falha ao executar a ação do evento!").WithSupportMessage("Aqui será uma mensagem com mais detalhes do erro.").WithMessageType(ActionMessageTypeEnum.Error))))))
                )
            .AddDataView(x => x.WithIdentifier("dataViewUsers").WithTitle("Listagem de Usuários").WithIndex(true)
                .AddTable(e => e.WithIdentifier("tableUsers")
                    .SetColumn(c => c.WithProperty(nameof(Usuario)))
                    .SetColumn(c => c.WithProperty(nameof(Nome)))
                    .SetColumn(c => c.WithProperty(nameof(Email)))
                    //.SetColumn(c => c.WithProperty(nameof(GlbImagemObjectEntity.Imagem)))
                    .SetColumn(c => c.WithProperty(nameof(DataInicio)))
                    .SetColumn(c => c.WithProperty(nameof(CodAcesso)))
                    //.SetColumn(c => c.WithProperty(nameof(DescricaoAcesso)))
                    //.SetColumn(c => c.WithProperty(nameof(Apelido)))
                    //.SetColumn(c => c.WithProperty(nameof(Chapa)))
                    .SetFilter(f => f.WithProperty(nameof(Usuario)).WithBasicOperator(FilterOperatorEnum.Equal, false))
                    .SetFilter(f =>
                        f.WithProperty(nameof(Nome))
                            .WithAdvancedOperator([FilterOperatorEnum.Equal, FilterOperatorEnum.Contains],
                                false)))
                .Configuration(c => c                    
                    .ExcludeProperty(ep => ep.WithProperty(nameof(Senha)))
                    .ExcludeProperty(ep => ep.WithProperty(nameof(Controle)))
                    .ExcludeProperty(ep => ep.WithProperty(nameof(UserId)))
                    .ExcludeProperty(ep => ep.WithProperty(nameof(DataExpiracao)))
                    .ExcludeProperty(ep => ep.WithProperty(nameof(DiasExpSenha)))
                    .ExcludeProperty(ep => ep.WithProperty(nameof(DataExpSenha)))
                    .ExcludeProperty(ep => ep.WithProperty(nameof(ObrigaAlterarSenha)))
                    .ExcludeProperty(ep => ep.WithProperty(nameof(IgnorarAutenticacaoLdap)))
                    .ExcludeProperty(ep => ep.WithProperty(nameof(Permis)))
                    .ExcludeProperty(ep => ep.WithProperty(nameof(Perfis)))
                    .AddParameter(p =>
                        p.WithName(nameof(Usuario)).WithType(typeof(string)).WithRequired(true)
                            .WithSource(ParameterSourceEnum.PathVariable).WithDescription("Código do Usuário"))
                    .AddFilter(p => p.WithName(nameof(Usuario)).WithType(typeof(string)).WithRequired(true)
                        .WithSource(ParameterSourceEnum.PathVariable).WithDescription("Código do Usuário")
                        .WithOperator(FilterOperatorEnum.Equal).WithParameter(nameof(Usuario)))
                    .AddPageAction(pa => pa
                        .AddAction(a => a
                            .AddShowMessageAction(r => r.WithIdentifier("pageNavHelloWorld").WithLabel("Hello World")
                                .WithMessage("Hello World").WithMessageType(ActionMessageTypeEnum.Warning))))
                    .AddPageAction(pa => pa
                        .AddAction(a => a
                            .AddNavigateAction(n =>
                                n.WithIdentifier("pageCallSiteTotvs").WithLabel("TOTVS").WithUrl("https://totvs.com.br")
                                    .WithTarget(ActionTargetEnum.Blank))))
                    .AddTableAction(ta => ta.WithMinSelectedItems(1).WithMaxSelectedItems(3)
                        .AddAction(a => a
                            .AddShowMessageAction(r =>
                                r.WithIdentifier("tableNavNameUser").WithLabel("Clique aqui!")
                                    .WithMessageType(ActionMessageTypeEnum.Info)
                                    .WithMessage(string.Format("Olá! O nome do usuário selecionado é: {0}.", "{{$selectedRow.nome}}")))))
                    .AddTableAction(ta => ta.WithMinSelectedItems(2).WithMaxSelectedItems(4)
                        .AddAction(a => a
                            .AddApiCallAction(n =>
                                n.WithIdentifier("tableCallDeleteUser").WithLabel("Excluir Usuário")
                                    .WithMethod(ActionMethodEnum.DELETE)
                                    .WithEndpoint("/data/GlbUsuarioModel/{{$selectedRow.Usuario}}")))))
            )
            .AddDataNew(v => v.WithIdentifier("dataNewUser").WithTitle("Novo Usuário")
                .AddTabs(t => t
                    .AddTab(tab => tab.WithIdentifier("tabUser").WithTitle("Usuário").WithOrder(1)
                        .AddSection(s => s.WithIdentifier("sectionDataUser").WithTitle("Dados do Usuário").WithOrder(1)
                            .AddElement(e => e.WithProperty(nameof(Usuario)))
                            .AddElement(e => e.WithProperty(nameof(Nome)))
                            .AddElement(e => e.WithProperty(nameof(Email)))
                            .AddElement(e => e.WithProperty(nameof(UserId)))
                            .AddElement(e => e.WithProperty(nameof(Status)))
                            .AddElement(e => e.WithProperty(nameof(DataInicio)))
                            .AddElement(e => e.WithProperty(nameof(DataExpiracao))))
                        .AddSection(s => s.WithIdentifier("sectionAccessUser").WithTitle("Dados de Acesso").WithOrder(3)
                            .AddElement(e => e.WithProperty(nameof(CodAcesso)))
                            .AddElement(e => e.WithProperty(nameof(Senha)))
                            .AddElement(e => e.WithProperty(nameof(Controle)))
                            .AddElement(e => e.WithProperty(nameof(DataExpSenha)))
                            .AddElement(e => e.WithProperty(nameof(DiasExpSenha)))
                            .AddElement(e => e.WithProperty(nameof(IgnorarAutenticacaoLdap)))
                            .AddElement(e => e.WithProperty(nameof(ObrigaAlterarSenha)))))
                    .AddTab(s => s.WithIdentifier("tabPemisUser").WithTitle("Permissões").WithOrder(2)
                        .AddSection(e => e.WithIdentifier("tablePermis").WithTitle("Permissões")
                            .AddElement(c => c.WithProperty(nameof(Permis)))))
                    .AddTab(s => s.WithIdentifier("tabPerfiUser").WithTitle("Perfis").WithOrder(3)
                        .AddSection(e => e.WithIdentifier("tablePerfis").WithTitle("Perfis")
                            .AddElement(c => c.WithProperty(nameof(Perfis))))))
                .Configuration(c => c
                    .ExcludeProperty(ep => ep.WithProperty(nameof(UserId)))
                    // .ExcludeProperty(ep => ep.WithProperty(nameof(DescricaoAcesso)))
                    // .ExcludeProperty(ep => ep.WithProperty(nameof(Apelido)))
                    // .ExcludeProperty(ep => ep.WithProperty(nameof(Chapa)))
                    .AddPageAction(pa => pa
                        .AddAction(a => a
                            .AddNavigateAction(na => na
                                .WithIdentifier("siteTotvs").WithLabel("Totvs").WithTarget(ActionTargetEnum.Blank)
                                .WithUrl("https://totvs.com"))))
                    .AddPageAction(pa => pa
                        .AddAction(a => a
                            .AddShowMessageAction(sm =>
                                sm.WithLabel("Hello World!").WithMessage("Hello World!")
                                    .WithMessageType(ActionMessageTypeEnum.Warning)))))
            )
            .AddDataDetail(v => v.WithIdentifier("dataDetailUser").WithTitle("Detalhes do Usuário")
                    .AddTabs(t => t
                        .AddTab(tab => tab.WithIdentifier("tabUser").WithTitle("Usuário").WithOrder(1)
                            .AddTabs(subTabs => subTabs.WithOrder(1)
                                .AddTab(subTab => subTab.WithIdentifier("subTabUserData").WithTitle("Dados do Usuário")
                                    .WithOrder(1)
                                    .AddSection(s => s.WithIdentifier("sectionDataUser").WithTitle("Dados do Usuário")
                                        .WithOrder(2)
                                        .AddElement(e => e.WithProperty(nameof(Usuario)))
                                        .AddElement(e => e.WithProperty(nameof(Nome)))
                                        .AddElement(e => e.WithProperty(nameof(Email)))
                                        .AddElement(e => e.WithProperty(nameof(UserId)))
                                        .AddElement(e => e.WithProperty(nameof(Status)))
                                        .AddElement(e => e.WithProperty(nameof(DataInicio)))
                                        .AddElement(e => e.WithProperty(nameof(DataExpiracao)))))
                                .AddTab(subTab => subTab.WithIdentifier("subTabAccessData").WithTitle("Dados de Acesso")
                                    .WithOrder(1)
                                    .AddSection(s => s.WithIdentifier("sectionAccessUser").WithTitle("Dados de Acesso")
                                        .WithOrder(1)
                                        .AddElement(e => e.WithProperty(nameof(CodAcesso)))
                                        .AddElement(e => e.WithProperty(nameof(Senha)))
                                        .AddElement(e => e.WithProperty(nameof(Controle)))
                                        .AddElement(e => e.WithProperty(nameof(IgnorarAutenticacaoLdap)))
                                        .AddSection(subSection => subSection.WithIdentifier("subSectionControlPass")
                                            .WithTitle("Controle de Senha").WithOrder(1)
                                            .AddElement(e => e.WithProperty(nameof(DataExpSenha)))
                                            .AddElement(e => e.WithProperty(nameof(DiasExpSenha)))
                                            .AddElement(e => e.WithProperty(nameof(ObrigaAlterarSenha))))))))
                        .AddTab(s => s.WithIdentifier("tabPemisUser").WithTitle("Permissões").WithOrder(2)
                            .AddSection(e => e.WithIdentifier("tablePermis").WithTitle("Permissões")
                                .AddElement(c => c.WithProperty(nameof(Permis)))))
                        .AddTab(s => s.WithIdentifier("tabPerfiUser").WithTitle("Perfis").WithOrder(3)
                            .AddSection(e => e.WithIdentifier("tablePerfis").WithTitle("Perfis")
                                .AddElement(c => c.WithProperty(nameof(Perfis))))))
                    .AddSection(s => s.WithIdentifier("relationalFields").WithTitle("Campos Relacionais").WithOrder(2)
                    // .AddElement(e => e.WithProperty(nameof(DescricaoAcesso)))
                    // .AddElement(e => e.WithProperty(nameof(Apelido)))
                    // .AddElement(e => e.WithProperty(nameof(Chapa))))
                    )
                    // .AddDataEdit(v => v.WithIdentifier("dataEditUser").WithTitle("Editar Usuário")
                    .AddTabs(t => t
                        .AddTab(tab => tab.WithIdentifier("tabUser").WithTitle("Usuário").WithOrder(1)
                            .AddSection(s => s.WithIdentifier("sectionDataUser").WithTitle("Dados do Usuário")
                                .AddElement(e => e.WithProperty(nameof(Usuario)))
                                .AddElement(e => e.WithProperty(nameof(Nome)))
                                .AddElement(e => e.WithProperty(nameof(Email)))
                                .AddElement(e => e.WithProperty(nameof(UserId)))
                                .AddElement(e => e.WithProperty(nameof(Status)))
                                .AddElement(e => e.WithProperty(nameof(DataInicio)))
                                .AddElement(e => e.WithProperty(nameof(DataExpiracao))))
                            .AddSection(s => s.WithIdentifier("sectionAccessUser").WithTitle("Dados de Acesso")
                                .AddElement(e => e.WithProperty(nameof(CodAcesso)))
                                .AddElement(e => e.WithProperty(nameof(Senha)))
                                .AddElement(e => e.WithProperty(nameof(Controle)))
                                .AddElement(e => e.WithProperty(nameof(DataExpSenha)))
                                .AddElement(e => e.WithProperty(nameof(DiasExpSenha)))
                                .AddElement(e => e.WithProperty(nameof(IgnorarAutenticacaoLdap)))
                                .AddElement(e => e.WithProperty(nameof(ObrigaAlterarSenha)))))
                        .AddTab(s => s.WithIdentifier("tabPemisUser").WithTitle("Permissões").WithOrder(2)
                            .WithDisabled()
                            .AddSection(e => e.WithIdentifier("tablePermis").WithTitle("Permissões")
                                .AddElement(c => c.WithProperty(nameof(Permis)))))
                        .AddTab(s => s.WithIdentifier("tabPerfiUser").WithTitle("Perfis").WithOrder(3)
                            .AddSection(e => e.WithIdentifier("tablePerfis").WithTitle("Perfis")
                                .AddElement(c => c.WithProperty(nameof(Perfis))))))
                    .AddSection(s => s.WithIdentifier("relationalFields").WithTitle("Campos Relacionais").WithOrder(2))
            // .AddElement(e => e.WithProperty(nameof(DescricaoAcesso)))
            // .AddElement(e => e.WithProperty(nameof(Apelido)))
            // .AddElement(e => e.WithProperty(nameof(Chapa))))
            );
    }
}
