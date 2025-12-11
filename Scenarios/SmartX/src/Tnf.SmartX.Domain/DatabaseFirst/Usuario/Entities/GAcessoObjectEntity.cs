namespace Tnf.SmartX.Domain.DatabaseFirst.Usuario.Entities;

[SxObject("GlbAcessoObject", "GACESSO", "Cadastro de Acessos")]
[SXConstraint(Name = "pk_gacesso", Type = "primarykey", Columns = new[] { "CODACESSO" })]
[SxFinder(new[] { nameof(CodAcesso) })]
public class GAcessoObjectEntity : SXObject
{
    [SxProperty(ColumnName = "CODACESSO", Title = "Código do acesso", Description = "Código do acesso", Required = true, IsPrimaryKey = true, MaxLength = 16)]
    public string CodAcesso { get; set; }

    [SxProperty(ColumnName = "DESCRICAO", Title = "Descrição", Description = "Descrição do acesso", Required = true, MaxLength = 40)]
    public string Descricao { get; set; }

    [SxProperty(FieldType = FieldType.NavigationProperty)]
    public virtual ICollection<UsuarioObjectEntity> Usuarios { get; set; } = new List<UsuarioObjectEntity>();
}
