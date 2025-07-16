using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Tnf.SmartX.EntityFramework.PostgreSql.Migrations.PostgreSqlCompanyDb
{
    /// <inheritdoc />
    public partial class InitialCompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    TradeName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    RegistrationNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    HasEsg = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    EmailAddress = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Position = table.Column<string>(type: "text", nullable: false),
                    TeamId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Email", "LastModificationTime", "Name", "RegistrationNumber", "TenantId", "TradeName" },
                values: new object[] { new Guid("21620343-c576-4a81-8e5e-02548d517e56"), "info@globex.com", null, "Globex Corporation", null, new Guid("6bb15e79-b912-481c-9a77-bed8aa1d7247"), null });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Email", "HasEsg", "LastModificationTime", "Name", "RegistrationNumber", "TenantId", "TradeName" },
                values: new object[] { new Guid("aa6a3c9e-878d-4c82-b556-9383399b84d7"), "contact@acme.com", true, null, "ACME Corporation", null, new Guid("6bb15e79-b912-481c-9a77-bed8aa1d7247"), null });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "CompanyId", "LastModificationTime", "Name" },
                values: new object[,]
                {
                    { new Guid("377c28fe-886b-4526-aa4f-561f6483a89b"), new Guid("aa6a3c9e-878d-4c82-b556-9383399b84d7"), null, "IT" },
                    { new Guid("5438b190-454e-407e-b35c-53b196bb97de"), new Guid("21620343-c576-4a81-8e5e-02548d517e56"), null, "Finance" },
                    { new Guid("783fbd9f-1909-4dd2-b0b1-6fc15802ec5a"), new Guid("aa6a3c9e-878d-4c82-b556-9383399b84d7"), null, "HR" }
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "DepartmentId", "Name" },
                values: new object[,]
                {
                    { new Guid("122289fe-d0f5-480b-a9c1-bc129d7b0181"), new Guid("377c28fe-886b-4526-aa4f-561f6483a89b"), "QA" },
                    { new Guid("5f605b21-ebeb-41bc-8cf5-f8cc539f668f"), new Guid("377c28fe-886b-4526-aa4f-561f6483a89b"), "Development" },
                    { new Guid("78555144-3ccf-4665-b991-beeee015db41"), new Guid("783fbd9f-1909-4dd2-b0b1-6fc15802ec5a"), "Recruitment" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "EmailAddress", "FullName", "Position", "TeamId" },
                values: new object[,]
                {
                    { new Guid("0b36f7e8-6b75-499b-9b35-d579e6564c2c"), "jane.smith@acme.com", "Jane Smith", "HR Specialist", new Guid("78555144-3ccf-4665-b991-beeee015db41") },
                    { new Guid("5228f999-0193-42d0-aff7-400648cd26ee"), "john.doe@acme.com", "John Doe", "Developer", new Guid("5f605b21-ebeb-41bc-8cf5-f8cc539f668f") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_CompanyId",
                table: "Departments",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_TeamId",
                table: "Employees",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_DepartmentId",
                table: "Teams",
                column: "DepartmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
