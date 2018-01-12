using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Case2.Infra.Migrations
{
    public partial class Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            for (int i = 1; i <= 50; i++)
            {
                migrationBuilder.Sql($"INSERT INTO Customers (Id, Name, Email) VALUES('{Guid.NewGuid()}', 'Customer {i}', 'customer{i}@totvs.com.br')");
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Customers");
        }
    }
}
