using Acme.SimpleTaskApp.Crud;
using Acme.SimpleTaskApp.People;
using Acme.SimpleTaskApp.Courses;
using Acme.SimpleTaskApp.Tasks;
using Microsoft.EntityFrameworkCore;
using Tnf.EntityFrameworkCore;
using Abp.EntityFramework;
using Tnf.Domain.Repositories;
using Abp.EntityFrameworkCore.Repositories;

namespace Acme.SimpleTaskApp.EntityFrameworkCore
{
    public class SimpleTaskAppDbContext : TnfDbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Task> Tasks { get; set; }

        //DbSet de Crud
        public DbSet<Customer> Customer { get; set; }

        public SimpleTaskAppDbContext(DbContextOptions<SimpleTaskAppDbContext> options)
            : base(options)
        {

        }
    }
}
