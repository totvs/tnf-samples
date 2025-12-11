using Tnf.EntityFrameworkCore;
using Tnf.Runtime.Session;
using Tnf.SmartX.Domain.CodeFirst.Entities;
using Tnf.SmartX.EntityFramework.Configurations;

namespace Microsoft.EntityFrameworkCore;

public class CompanyDbContext(DbContextOptions options, ITnfSession session) : TnfDbContext(options, session)
{
    public DbSet<CompanyEntity> Companies { get; set; }
    public DbSet<DepartmentEntity> Departments { get; set; }
    public DbSet<EmployeeEntity> Employees { get; set; }
    public DbSet<TeamEntity> Teams { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new CompanyEntityConfiguration());
        modelBuilder.ApplyConfiguration(new DepartmentEntityConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeeEntityConfiguration());
        modelBuilder.ApplyConfiguration(new TeamEntityConfiguration());

        modelBuilder.SeedData();

    }
}

public static class DatabaseSeedingExtensions
{
    public static void SeedData(this ModelBuilder modelBuilder)
    {
        var tenant1Id = Guid.Parse("6BB15E79-B912-481C-9A77-BED8AA1D7247");

        var company1Id = Guid.Parse("AA6A3C9E-878D-4C82-B556-9383399B84D7");
        var company2Id = Guid.Parse("21620343-C576-4A81-8E5E-02548D517E56");

        var deptItId = Guid.Parse("377C28FE-886B-4526-AA4F-561F6483A89B");
        var deptHrId = Guid.Parse("783FBD9F-1909-4DD2-B0B1-6FC15802EC5A");
        var deptFinId = Guid.Parse("5438B190-454E-407E-B35C-53B196BB97DE");

        var teamDevId = Guid.Parse("5F605B21-EBEB-41BC-8CF5-F8CC539F668F");
        var teamQaId = Guid.Parse("122289FE-D0F5-480B-A9C1-BC129D7B0181");
        var teamRecId = Guid.Parse("78555144-3CCF-4665-B991-BEEEE015DB41");

        var empJohnId = Guid.Parse("5228F999-0193-42D0-AFF7-400648CD26EE");
        var empJaneId = Guid.Parse("0B36F7E8-6B75-499B-9B35-D579E6564C2C");

        modelBuilder.Entity<CompanyEntity>().HasData(
            new CompanyEntity
            {
                Id = company1Id,
                TenantId = tenant1Id,
                Name = "ACME Corporation",
                HasEsg = true,
                Email = "contact@acme.com"
            },
            new CompanyEntity
            {
                Id = company2Id,
                TenantId = tenant1Id,
                Name = "Globex Corporation",
                HasEsg = false,
                Email = "info@globex.com"
            }
        );

        modelBuilder.Entity<DepartmentEntity>().HasData(
            new DepartmentEntity
            {
                Id = deptItId,
                Name = "IT",
                CompanyId = company1Id
            },
            new DepartmentEntity
            {
                Id = deptHrId,
                Name = "HR",
                CompanyId = company1Id
            },
            new DepartmentEntity
            {
                Id = deptFinId,
                Name = "Finance",
                CompanyId = company2Id
            }
        );

        modelBuilder.Entity<TeamEntity>().HasData(
            new TeamEntity
            {
                Id = teamDevId,
                Name = "Development",
                DepartmentId = deptItId
            },
            new TeamEntity
            {
                Id = teamQaId,
                Name = "QA",
                DepartmentId = deptItId
            },
            new TeamEntity
            {
                Id = teamRecId,
                Name = "Recruitment",
                DepartmentId = deptHrId
            }
        );

        modelBuilder.Entity<EmployeeEntity>().HasData(
            new EmployeeEntity
            {
                Id = empJohnId,
                FullName = "John Doe",
                EmailAddress = "john.doe@acme.com",
                Position = "Developer",
                TeamId = teamDevId
            },
            new EmployeeEntity
            {
                Id = empJaneId,
                FullName = "Jane Smith",
                EmailAddress = "jane.smith@acme.com",
                Position = "HR Specialist",
                TeamId = teamRecId
            }
        );
    }
}
