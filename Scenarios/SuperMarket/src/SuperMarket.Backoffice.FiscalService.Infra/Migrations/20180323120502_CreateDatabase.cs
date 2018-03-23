using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SuperMarket.Backoffice.FiscalService.Infra.Migrations
{
    public partial class CreateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PurchaseOrderTaxMoviments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OrderBaseValue = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    OrderCustomer = table.Column<Guid>(nullable: false),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    OrderNumber = table.Column<Guid>(nullable: false),
                    OrderTotalValue = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    Percentage = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderTaxMoviments", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurchaseOrderTaxMoviments");
        }
    }
}
