namespace Tnf.SmartX.Domain.DatabaseFirst.Usuario.Entities;

/// <summary>
///     Objeto as permissões por sistema.
/// </summary>
[SxObject("GlbSegurancaPermisObject", "GPERMIS", "Cadastro de Permissõe")]
[SXConstraint(Name = "pk_gperfil", Type = "primarykey", Columns = new[] { "CODCOLIGADA", "CODSISTEMA", "CODUSUARIO" })]
[SXConstraint(Name = "fk_gpermis_usuario", Type = "foreignkey", Columns = new[] { "CODUSUARIO" }, ParentObjectName = "GUSUARIO", ParentColumns = new[] { "CODUSUARIO" })]
[SxFinder(new[] { nameof(CodColigada), nameof(CodSistema), nameof(CodUsuario) }, '|', "{0}|{1}|{2}")]
public class PermisObjectEntity : SXObject
{
    [SxProperty(ColumnName = "CODCOLIGADA", Title = "Código da coligada", Description = "Código da coligada",
        Required = true, IsPrimaryKey = true)]
    public int CodColigada { get; set; }

    [SxProperty(ColumnName = "CODSISTEMA", Title = "Cód.Sistema", Description = "Cód.Sistema", MaxLength = 1,
        Required = true, IsPrimaryKey = true)]
    [SxFixedValues(new object[]
    {
        "0", "A", "B", "C", "D", "E", "F", "G", "H", "I", "K", "L", "M", "N", "O", "P", "R", "S", "T", "U", "V",
        "W", "X", "Y"
    })]
    public string CodSistema { get; set; }

    [SxProperty(ColumnName = "CODUSUARIO", Title = "Código do usuario", Description = "Código do usuario",FieldType = FieldType.FKFieldProperty,  MaxLength = 20)]
    public string CodUsuario { get; set; }

    [SxProperty(ColumnName = "SUPERVISOR", Title = "Supervisor", Description = "Supervisor")]
    [SxOptionValues(new object[] { 0, 1 }, typeof(string), new[] { "No", "Yes" })]
    public int Supervisor { get; set; }

    [SxProperty(ColumnName = "CRIARELAT", Title = "Pode Incluir Relatórios",
        Description = "Pode Incluir Relatórios")]
    [SxOptionValues(new object[] { 0, 1 }, typeof(string), new[] { "No", "Yes" })]
    public int CriaRelat { get; set; }

    [SxProperty(ColumnName = "CONTROLE", Title = "Controle", Description = "Controle", Hidden = true)]
    public short? Controle { get; set; }

    // [SxRelationAttribute(Name = "fk_GUsuario_GPermis", Title = "Usuário", Columns = new string[] { "CODUSUARIO" }, ParentColumns = new string[] { "CODUSUARIO" })]
    [SxProperty(FieldType = FieldType.NavigationProperty)]
    public UsuarioObjectEntity Usuario { get; set; }
}
