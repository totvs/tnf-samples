using Case2.Infra.Context.Migration;
using Case2.Infra.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Linq;

namespace Case2.Infra.Migrations.EmployeeDb
{
    public partial class SeedEmployees : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var designFactory = new EmployeeDbContextFactory();

            using (var db = designFactory.CreateDbContext(new string[] { }))
            {
                for (int i = 0; i < 50; i++)
                {
                    db.Employees.Add(new Employee()
                    {
                        Name = $"Employee {i}",
                        Email = $"employee{i}@totvs.com"
                    });
                }

                db.SaveChanges();
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var designFactory = new EmployeeDbContextFactory();

            using (var db = designFactory.CreateDbContext(new string[] { }))
            {
                var employees = db.Employees.IgnoreQueryFilters().ToArray();

                db.Employees.RemoveRange(employees);

                db.SaveChanges();
            }
        }
    }
}
