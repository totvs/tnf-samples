using Case2.Infra.Entities;
using Microsoft.EntityFrameworkCore;
using Tnf.EntityFrameworkCore;
using Tnf.Runtime.Session;

namespace Case2.Infra.Context
{
    public class EmployeeDbContext : TnfDbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public EmployeeDbContext(DbContextOptions options, ITnfSession session)
            : base(options, session)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(m =>
            {
                m.HasKey(k => k.Id);
                m.Property(p => p.Name).IsRequired();
            });
        }
    }
}
