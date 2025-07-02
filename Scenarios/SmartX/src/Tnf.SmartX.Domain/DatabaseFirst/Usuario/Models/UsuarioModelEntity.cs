
using Tnf.SmartX.Domain.DatabaseFirst.Usuario.Entities;

namespace Tnf.SmartX.Domain.DatabaseFirst.Usuario.Models;

[SxModel(ModelName = "GlbUsuarioModel", Description = "Usuários")]
public class UsuarioModelEntity : UsuarioObjectEntity, ISXModel
{
    /// <summary>
    ///     Lista de permissões do usuário
    /// </summary>
    [SxRelation(Name = "fk_GUsuario_GPermis", Title = "Permissões", Columns = new[] { "CODUSUARIO" }, ParentColumns = new[] { "CODUSUARIO" })]
    public List<PermisObjectEntity> Permis { get; set; } = new();

    /// <summary>
    ///     Lista de perfis do usuário
    /// </summary>
    [SxRelation(Name = "fk_GUsuario_GUsrPerfil", Title = "Perfis", Columns = new[] { "CODUSUARIO" }, ParentColumns = new[] { "CODUSUARIO" })]
    public List<UsuarioPerfilObjectEntity> Perfis { get; set; } = new();

    /// <summary>
    ///     Configuração do modelo de negócio
    /// </summary>
    /// <param name="config"></param>
    protected override void DoConfigure(IConfigurationModelBuilder builder)
    {
        base.DoConfigure(builder);

        builder.SetProperty(p => p.WithProperty(nameof(Permis))
            .SetChildProperty(c =>
                c.WithProperty(nameof(PermisObjectEntity.Supervisor))
                    .WithOptionValues(
                        new[]
                        {
                            new OptionResult { Value = 1, Description = "Yes" },
                            new OptionResult { Value = 0, Description = "No" }
                        }
                    )));

        builder.SetProperty(p => p
                .WithProperty("codAcesso")
                .AddLookup(l =>
                  l.WithModelRef("GlbAcessoModel")
                    .WithDisplayFields(new[] { "descricao" })
                    .WithFieldValue("codAcesso")
                    .WithLookupFinder(new FinderResult(new[] { "codAcesso" })))
                );

        builder.SetProperty(p => p.WithProperty(nameof(Status))
                .WithOptionValues(
                    [
                        new OptionResult { Value = 0, Description = "Inativo"},
                        new OptionResult { Value = 1, Description = "Ativo" },
                    ]));
    }

    protected override void DoBeforeCreate(BeforeCreateParams parms)
    {
        Senha = "1234561";
        Controle = 1;
        UserId = Usuario;
        var indice = 0;
        if (Perfis != null)
        {
            foreach (var perfil in Perfis)
            {
                perfil.Indice = indice;
                indice++;
            }
        }
    }

    protected override bool DoOnValidate(OnValidateParams parms)
    {
        Usuario = null;

        return OnValidate(parms);
    }
}
