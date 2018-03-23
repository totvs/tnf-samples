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
                    Percentage = table.Column<int>(nullable: false),
                    PurchaseOrderBaseValue = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    PurchaseOrderDiscount = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    PurchaseOrderId = table.Column<Guid>(nullable: false),
                    PurchaseOrderTotalValue = table.Column<decimal>(type: "decimal(18, 6)", nullable: false),
                    Tax = table.Column<decimal>(type: "decimal(18, 6)", nullable: false)
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
