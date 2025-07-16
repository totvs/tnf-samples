using Tnf.SmartX.Domain.DatabaseFirst.Usuario.Models;

namespace Tnf.SmartX.Domain.DatabaseFirst.Usuario.Routines;

[SxRoutine(RoutineName = "GlbUsuarioRoutineDataEdit", Title = "UserRoutine", Version = "1.0")]
public class UsuarioRoutineUIDataEdit : UsuarioModelEntity, ISXRoutineLayoutDataEdit
{
    public void ConfigureLayout(IRoutineLayoutDataEditBuilder builder)
    {
        builder
            .AddDataEdit(v => v.WithIdentifier("dataEditlUser").WithTitle("Editar Usuário")
                    .WithElementsBase("dataNewUser")
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
                            .AddSection(e => e.WithIdentifier("tablePermis").WithTitle("Permissões")
                                .AddElement(c => c.WithProperty(nameof(Permis)))))
                        .AddTab(s => s.WithIdentifier("tabPerfiUser").WithTitle("Perfis").WithOrder(3)
                            .AddSection(e => e.WithIdentifier("tablePerfis").WithTitle("Perfis")
                                .AddElement(c => c.WithProperty(nameof(Perfis))))))
                // .Configuration(c => c
                //   .ExcludeProperty(ep => ep.WithProperty(nameof(DescricaoAcesso)))
                //   .ExcludeProperty(ep => ep.WithProperty(nameof(Apelido)))
                //   .ExcludeProperty(ep => ep.WithProperty(nameof(Chapa))))
            );
    }
}
