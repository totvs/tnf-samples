namespace Tnf.SmartX.Domain.DatabaseFirst.Usuario.Entities;

[SxObject("PPessoaObject", "PPESSOA", "Cadastro de Pessoas")]
[SXConstraint(Name = "pk_ppessoa", Type = "primarykey", Columns = new[] { "CODIGO" })]
[SxFinder(new[] { nameof(Codigo) })]
public class PPessoaObjectEntity : SXObject
{
    [SxProperty(ColumnName = "CODIGO", Title = "Código", Description = "Código", Required = true,
        IsPrimaryKey = true)]
    public int Codigo { get; set; }

    [SxProperty(ColumnName = "NOME", Title = "Nome", Description = "Nome")]
    public string Nome { get; set; }

    [SxProperty(ColumnName = "APELIDO", Title = "Apelido", Description = "Apelido")]
    public string Apelido { get; set; }

    [SxProperty(ColumnName = "CODUSUARIO", Title = "Código do usuario", Description = "Código do usuario")]
    public string CodUsuario { get; set; }
}
