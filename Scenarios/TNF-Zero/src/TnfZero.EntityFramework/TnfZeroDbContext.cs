using Microsoft.EntityFrameworkCore;
using Tnf.EntityFrameworkCore;
using Tnf.Runtime.Session;
using TnfZero.Domain.Entities;
using TnfZero.EntityFramework.Configurations;

namespace TnfZero.EntityFramework;

public class TnfZeroDbContext(DbContextOptions options, ITnfSession session)
    : TnfDbContext(options, session)
{
    public DbSet<DogEntity> Dogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new DogEntityConfiguration());
    }
}