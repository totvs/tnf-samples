namespace Tnf.SmartX.Domain.DatabaseFirst.Usuario.Entities;

[SxObject("PFuncObject", "PFUNC", "Cadastro de Funcionários")]
[SXConstraint(Name = "pk_pfunc", Type = "primarykey", Columns = new[] { "CODCOLIGADA", "CHAPA" })]
[SxFinder(new[] { nameof(CodColigada), nameof(Chapa) })]
public class PFuncObjectEntity : SXObject
{
    [SxProperty(ColumnName = "CODCOLIGADA", Title = "Código da coligada", Description = "Código da coligada",
        Required = true, IsPrimaryKey = true)]
    public int CodColigada { get; set; }

    [SxProperty(ColumnName = "CHAPA", Title = "Chapa", Description = "Chapa", Required = true, IsPrimaryKey = true)]
    public string Chapa { get; set; }

    [SxProperty(ColumnName = "CODPESSOA", Title = "Pessoa", Description = "Pessoa")]
    public int CodPessoa { get; set; }
}
