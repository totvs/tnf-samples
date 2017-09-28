using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Tnf.Architecture.EntityFrameworkCore.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SYS009_PROFESSIONAL",
                columns: table => new
                {
                    SYS009_PROFESSIONAL_ID = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    SYS009_PROFESSIONAL_CODE = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SYS009_ADDRESS = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SYS009_ADDRESS_COMPLEMENT = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SYS009_ADDRESS_NUMBER = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    SYS009_EMAIL = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SYS009_NAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SYS009_PHONE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SYS009_ZIP_CODE = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYS009_PROFESSIONAL", x => new { x.SYS009_PROFESSIONAL_ID, x.SYS009_PROFESSIONAL_CODE });
                });

            migrationBuilder.CreateTable(
                name: "SYS011_SPECIALTIES",
                columns: table => new
                {
                    SYS011_SPECIALTIES_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SYS011_SPECIALTIES_DESCRIPTION = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYS011_SPECIALTIES", x => x.SYS011_SPECIALTIES_ID);
                });

            migrationBuilder.CreateTable(
                name: "SYS010_PROFESSIONAL_SPECIALTIES",
                columns: table => new
                {
                    SYS009_PROFESSIONAL_ID = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    SYS009_PROFESSIONAL_CODE = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SYS011_SPECIALTIES_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYS010_PROFESSIONAL_SPECIALTIES", x => new { x.SYS009_PROFESSIONAL_ID, x.SYS009_PROFESSIONAL_CODE, x.SYS011_SPECIALTIES_ID });
                    table.ForeignKey(
                        name: "FK_SYS010_PROFESSIONAL_SPECIALTIES_SYS011_SPECIALTIES_SYS011_SPECIALTIES_ID",
                        column: x => x.SYS011_SPECIALTIES_ID,
                        principalTable: "SYS011_SPECIALTIES",
                        principalColumn: "SYS011_SPECIALTIES_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SYS010_PROFESSIONAL_SPECIALTIES_SYS009_PROFESSIONAL_SYS009_PROFESSIONAL_ID_SYS009_PROFESSIONAL_CODE",
                        columns: x => new { x.SYS009_PROFESSIONAL_ID, x.SYS009_PROFESSIONAL_CODE },
                        principalTable: "SYS009_PROFESSIONAL",
                        principalColumns: new[] { "SYS009_PROFESSIONAL_ID", "SYS009_PROFESSIONAL_CODE" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SYS010_PROFESSIONAL_SPECIALTIES_SYS011_SPECIALTIES_ID",
                table: "SYS010_PROFESSIONAL_SPECIALTIES",
                column: "SYS011_SPECIALTIES_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SYS010_PROFESSIONAL_SPECIALTIES");

            migrationBuilder.DropTable(
                name: "SYS011_SPECIALTIES");

            migrationBuilder.DropTable(
                name: "SYS009_PROFESSIONAL");
        }
    }
}
