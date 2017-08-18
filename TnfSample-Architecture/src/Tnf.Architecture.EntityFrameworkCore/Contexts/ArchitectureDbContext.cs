using Microsoft.EntityFrameworkCore;
using Tnf.Architecture.Domain.Registration;
using Tnf.Architecture.EntityFrameworkCore.Entities;
using Tnf.EntityFrameworkCore;

namespace Tnf.Architecture.EntityFrameworkCore.Contexts
{
    public class ArchitectureDbContext : TnfDbContext
    {
        public DbSet<CountryPoco> Countries { get; set; }

        public DbSet<Person> People { get; set; }

        public ArchitectureDbContext(DbContextOptions<ArchitectureDbContext> options)
            : base(options)
        {
        }
    }
}
