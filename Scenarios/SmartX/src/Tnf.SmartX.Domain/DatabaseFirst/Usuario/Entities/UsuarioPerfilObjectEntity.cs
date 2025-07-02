namespace Tnf.SmartX.Domain.DatabaseFirst.Usuario.Entities;

/// <summary>
///     Objeto que representa a tabela  GUsuario.
/// </summary>
[SxObject("GlbSegurancaUsuarioPerfilObject", "GUSRPERFIL", "Cadastro de Perfis")]
[SXConstraint(Name = "pk_gusrperfil", Type = "primarykey", Columns = new[] { "CODCOLIGADA", "CODUSUARIO", "CODSISTEMA", "CODPERFIL" })]
[SXConstraint(Name = "fk_gusrperfil_gacesso", Type = "foreignkey", Columns = new[] { "CODACESSO" }, ParentObjectName = "GACESSO", ParentColumns = new[] { "CODACESSO" })]
[SxFinder(new[] { nameof(CodColigada), nameof(CodUsuario), nameof(CodSistema), nameof(CodPerfil) }, '|', "{0}|{1}|{2}|{3}")]
public class UsuarioPerfilObjectEntity : SXObject
{
    [SxProperty(ColumnName = "CODCOLIGADA", Title = "Código da coligada", Description = "Código da coligada", Required = true, IsPrimaryKey = true)]
    public int CodColigada { get; set; }

    [SxProperty(ColumnName = "CODUSUARIO", Title = "Código do usuario", Description = "Código do usuario", IsPrimaryKey = true, MaxLength = 20)]
    public string CodUsuario { get; set; }

    [SxProperty(ColumnName = "CODSISTEMA", Title = "Cód.Sistema", Description = "Cód.Sistema", MaxLength = 1, Required = true, IsPrimaryKey = true)]
    [SxFixedValues(new object[]
    {
        "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "K", "L", "M", "N", "O", "P", "R", "S", "T", "U", "V",
        "W", "X", "Y"
    })]
    public string CodSistema { get; set; }

    [SxProperty(ColumnName = "CODPERFIL", Title = "CodPerfil", Description = "CodPerfil", MaxLength = 15,
        IsPrimaryKey = true, Required = true)]
    public string CodPerfil { get; set; }

    [SxProperty(ColumnName = "CONTROLE", Title = "Controle", Description = "Controle", Hidden = true)]
    public short? Controle { get; set; }

    [SxProperty(ColumnName = "INDICE", Title = "Índice", Description = "Índice", Hidden = true)]
    public int? Indice { get; set; }

    [SxProperty(ColumnName = "CODACESSO", Title = "Código do acesso", Description = "Código do acesso", Required = true, FieldType = FieldType.FKFieldProperty)]
    public string CodAcesso { get; set; }

    [SxProperty(FieldType = FieldType.NavigationProperty)]
    public virtual GAcessoObjectEntity Acesso { get; set; }
}
