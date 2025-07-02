namespace Tnf.SmartX.Domain.DatabaseFirst.Usuario.Entities;

[SxObject("GlbUsuarioObject", "GUSUARIO", "Cadastro de Usuários")]
[SXConstraint(Name = "pk_gusuario", Type = "primarykey", Columns = new[] { "CODUSUARIO" })]
[SXConstraint(Name = "fk_gusuario_gacesso", Type = "foreignkey", Columns = new[] { "CODACESSO" }, ParentObjectName = "GACESSO", ParentColumns = new[] { "CODACESSO" })]
[SxFinder(new[] { nameof(Usuario) })]
public class UsuarioObjectEntity : SXObject
{
    [SxProperty(ColumnName = "CODUSUARIO", Title = "Código do usuario", Description = "Código do usuário", Required = true, IsPrimaryKey = true, MaxLength = 20)]
    public string Usuario { get; set; }

    [SxProperty(ColumnName = "NOME", Title = "Nome do usuário", Description = "Nome do usuário", Required = true)]
    public string Nome { get; set; }

    [SxProperty(ColumnName = "STATUS", Title = "Status", Description = "Status")]
    [SxFixedValues(new object[] { 0, 1 })]
    public int Status { get; set; }

    [SxProperty(ColumnName = "DATAINICIO", Title = "Data Inicio", Description = "Data Inicio")]
    public DateTime DataInicio { get; set; }

    [SxProperty(ColumnName = "DATAEXPIRACAO", Title = "Data Expiração", Description = "Data Expiração")]
    public DateTime? DataExpiracao { get; set; }

    [SxProperty(ColumnName = "SENHA", Title = "Senha", Description = "Senha", Hidden = true)]
    public string Senha { get; set; }

    [SxProperty(ColumnName = "CONTROLE", Title = "Controle", Description = "Controle")]
    public int? Controle { get; set; }

    [SxProperty(ColumnName = "DTAEXPSENHA", Title = "Data de expiração senha", Description = "Data de expiração senha")]
    public DateTime? DataExpSenha { get; set; }

    [SxProperty(ColumnName = "DIASEXPSENHA", Title = "Dias de expiração senha", Description = "Dias de expiração senha")]
    public int? DiasExpSenha { get; set; }

    [SxProperty(ColumnName = "EMAIL", Title = "Email", Description = "Email", Pattern = "[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$")]
    public string Email { get; set; }

    [SxProperty(ColumnName = "USERID", Title = "Identificador", Description = "UserId", Hidden = true)]
    public string UserId { get; set; }

    [SxProperty(
        ColumnName = "IGNORARAUTENTICACAOLDAP",
        Title = "Ignorar autenticação",
        Description = "Ignorar autenticação",
        DefaultValue = false,
        BooleanLabelTrue = "Sim",
        BooleanLabelFalse = "Não",
        Rules = new[] { RuleFieldEnum.rboolean }
    )]
    public bool? IgnorarAutenticacaoLdap { get; set; } = false;

    [SxProperty(
        ColumnName = "OBRIGAALTERARSENHA",
        Title = "Alterar senha",
        Description = "Alterar senha",
        DefaultValue = false,
        BooleanLabelTrue = "Sim",
        BooleanLabelFalse = "Não",
        Rules = new[] { RuleFieldEnum.rboolean }
     )]
    public bool? ObrigaAlterarSenha { get; set; } = false;

    [SxProperty(ColumnName = "CODACESSO", Title = "Cód.Acesso", Description = "Cód.Acesso", FieldType = FieldType.FKFieldProperty)]
    public string CodAcesso { get; set; }

    [SxProperty(FieldType = FieldType.NavigationProperty)]
    public virtual GAcessoObjectEntity AcessoObject { get; set; }

    [SxProperty(FieldType = FieldType.NavigationProperty)]
    public virtual ICollection<PermisObjectEntity> Permis { get; set; }
}
