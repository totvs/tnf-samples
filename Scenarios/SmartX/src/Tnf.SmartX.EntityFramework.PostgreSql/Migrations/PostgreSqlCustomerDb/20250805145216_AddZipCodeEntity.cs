#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Tnf.SmartX.EntityFramework.PostgreSql.Migrations.PostgreSqlCustomerDb;

/// <inheritdoc />
public partial class AddZipCodeEntity : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            "ZipCodes",
            table => new
            {
                ZipCode = table.Column<string>("text", nullable: false),
                Street = table.Column<string>("text", nullable: false),
                City = table.Column<string>("text", nullable: false),
                State = table.Column<string>("text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ZipCodes", x => x.ZipCode);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "ZipCodes");
    }
}
