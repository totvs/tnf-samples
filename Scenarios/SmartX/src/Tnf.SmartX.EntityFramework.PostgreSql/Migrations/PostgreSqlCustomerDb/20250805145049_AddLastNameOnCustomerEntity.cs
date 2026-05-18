#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Tnf.SmartX.EntityFramework.PostgreSql.Migrations.PostgreSqlCustomerDb;

/// <inheritdoc />
public partial class AddLastNameOnCustomerEntity : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            "LastName",
            "Customers",
            "text",
            nullable: false,
            defaultValue: "");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            "LastName",
            "Customers");
    }
}
