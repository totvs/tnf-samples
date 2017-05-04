using Microsoft.EntityFrameworkCore;
using Tnf.Architecture.EntityFrameworkCore.Entities;
using Tnf.EntityFrameworkCore;

namespace Tnf.Architecture.EntityFrameworkCore
{
    public class ArchitectureDbContext : TnfDbContext
    {
        public DbSet<Country> Countries { get; set; }

        public ArchitectureDbContext(DbContextOptions<ArchitectureDbContext> options)
            : base(options)
        {
        }


    }
}
