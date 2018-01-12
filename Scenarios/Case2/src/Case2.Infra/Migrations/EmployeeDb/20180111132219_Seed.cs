using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Case2.Infra.Migrations.EmployeeDb
{
    public partial class Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            for (int i = 1; i <= 50; i++)
            {
                migrationBuilder.Sql($"INSERT INTO Employees (Id, Name, Email) VALUES('{Guid.NewGuid()}', 'Employee {i}', 'employee{i}@totvs.com.br')");
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Employees");
        }
    }
}
