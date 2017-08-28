using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Tnf.Architecture.EntityFrameworkCore.Migrations.ArchitectureDb
{
    public partial class AdicionadoAutoRelacionamento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "People",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "People",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_People_ParentId",
                table: "People",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_People_People_Id",
                table: "People",
                column: "Id",
                principalTable: "People",
                principalColumn: "ParentId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_People_Id",
                table: "People");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_People_ParentId",
                table: "People");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "People");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "People",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
        }
    }
}
