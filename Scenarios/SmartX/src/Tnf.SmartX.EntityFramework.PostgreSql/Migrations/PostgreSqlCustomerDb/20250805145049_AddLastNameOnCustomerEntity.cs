using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tnf.SmartX.EntityFramework.PostgreSql.Migrations.PostgreSqlCustomerDb
{
    /// <inheritdoc />
    public partial class AddLastNameOnCustomerEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Customers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Customers");
        }
    }
}
