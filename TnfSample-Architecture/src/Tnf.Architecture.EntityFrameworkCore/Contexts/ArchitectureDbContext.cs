using Microsoft.EntityFrameworkCore;
using Tnf.Architecture.Domain.Registration;
using Tnf.EntityFrameworkCore;

namespace Tnf.Architecture.EntityFrameworkCore.Contexts
{
    public class ArchitectureDbContext : TnfDbContext
    {
        public DbSet<Person> People { get; set; }

        public ArchitectureDbContext(DbContextOptions<ArchitectureDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>(m =>
            {
                // PKs
                m.HasKey(i => i.Id);
                
                m.HasOne(o => o.Parent)
                    .WithMany(w => w.Children)
                    .HasPrincipalKey(k => k.Id)
                    .HasForeignKey(k => k.ParentId);
            });
        }
    }
}
