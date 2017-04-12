using Microsoft.EntityFrameworkCore;
using Tnf.EntityFrameworkCore;
using Tnf.Sample.Tasks;

namespace Tnf.Sample.EntityFrameworkCore
{
    public class SampleAppDbContext : TnfDbContext
    {
        public DbSet<Task> Tasks { get; set; }

        public SampleAppDbContext(DbContextOptions<SampleAppDbContext> options)
            : base(options)
        {

        }
    }
}
