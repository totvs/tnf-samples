#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Tnf.SmartX.EntityFramework.PostgreSql.Migrations.PostgreSqlCustomerDb;

/// <inheritdoc />
public partial class InitialCustomer : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            "Customers",
            table => new
            {
                Id = table.Column<Guid>("uuid", nullable: false),
                TenantId = table.Column<Guid>("uuid", nullable: false),
                Name = table.Column<string>("text", nullable: false),
                Email = table.Column<string>("text", nullable: false),
                PhoneNumber = table.Column<string>("text", nullable: false),
                CreationTime =
                    table.Column<DateTime>("timestamp without time zone", nullable: false,
                        defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                LastModificationTime = table.Column<DateTime>("timestamp without time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Customers", x => x.Id);
            });

        migrationBuilder.CreateTable(
            "States",
            table => new
            {
                UF = table.Column<string>("text", nullable: false),
                Description = table.Column<string>("text", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_States", x => x.UF);
            });

        migrationBuilder.CreateTable(
            "Addresses",
            table => new
            {
                Id = table.Column<Guid>("uuid", nullable: false),
                State = table.Column<string>("text", nullable: true),
                City = table.Column<string>("text", nullable: false),
                ZipCode = table.Column<string>("text", nullable: false),
                Street = table.Column<string>("text", nullable: false),
                CreationTime =
                    table.Column<DateTime>("timestamp without time zone", nullable: false,
                        defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                LastModificationTime = table.Column<DateTime>("timestamp without time zone", nullable: true),
                CustomerId = table.Column<Guid>("uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Addresses", x => x.Id);
                table.ForeignKey(
                    "FK_Addresses_Customers_CustomerId",
                    x => x.CustomerId,
                    "Customers",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "Deliveries",
            table => new
            {
                Id = table.Column<Guid>("uuid", nullable: false),
                ScheduledDate = table.Column<DateTime>("timestamp without time zone", nullable: false),
                Status = table.Column<string>("text", nullable: false),
                LastModificationTime = table.Column<DateTime>("timestamp without time zone", nullable: true),
                CreationTime =
                    table.Column<DateTime>("timestamp without time zone", nullable: false,
                        defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                AddressId = table.Column<Guid>("uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Deliveries", x => x.Id);
                table.ForeignKey(
                    "FK_Deliveries_Addresses_AddressId",
                    x => x.AddressId,
                    "Addresses",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "DeliveryItems",
            table => new
            {
                Id = table.Column<Guid>("uuid", nullable: false),
                ProductName = table.Column<string>("text", nullable: false),
                Quantity = table.Column<int>("integer", nullable: false),
                WeightKg = table.Column<decimal>("numeric", nullable: false),
                CreationTime =
                    table.Column<DateTime>("timestamp without time zone", nullable: false,
                        defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                LastModificationTime = table.Column<DateTime>("timestamp without time zone", nullable: true),
                DeliveryId = table.Column<Guid>("uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_DeliveryItems", x => x.Id);
                table.ForeignKey(
                    "FK_DeliveryItems_Deliveries_DeliveryId",
                    x => x.DeliveryId,
                    "Deliveries",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            "IX_Addresses_CustomerId",
            "Addresses",
            "CustomerId");

        migrationBuilder.CreateIndex(
            "IX_Deliveries_AddressId",
            "Deliveries",
            "AddressId");

        migrationBuilder.CreateIndex(
            "IX_DeliveryItems_DeliveryId",
            "DeliveryItems",
            "DeliveryId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "DeliveryItems");

        migrationBuilder.DropTable(
            "States");

        migrationBuilder.DropTable(
            "Deliveries");

        migrationBuilder.DropTable(
            "Addresses");

        migrationBuilder.DropTable(
            "Customers");
    }
}
