using Case4.Domain;
using Case4.Infra.Context.Migration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Linq;

namespace Case4.Infra.Migrations.CustomerDb
{
    public partial class SeedCustomers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var designFactory = new CustomerDbContextFactory();

            using (var db = designFactory.CreateDbContext(new string[] { }))
            {
                for (int i = 0; i < 50; i++)
                {
                    db.Customers.Add(new Customer()
                    {
                        Name = $"Customer {i}",
                        Email = $"customer{i}@totvs.com"
                    });
                }

                db.SaveChanges();
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var designFactory = new CustomerDbContextFactory();

            using (var db = designFactory.CreateDbContext(new string[] { }))
            {
                var customers = db.Customers.IgnoreQueryFilters().ToArray();

                db.Customers.RemoveRange(customers);

                db.SaveChanges();
            }
        }
    }
}
