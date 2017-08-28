using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Tnf.Architecture.EntityFrameworkCore.Contexts;

namespace Tnf.Architecture.EntityFrameworkCore.Migrations.ArchitectureDb
{
    [DbContext(typeof(ArchitectureDbContext))]
    [Migration("20170824175103_Adicionado Auto Relacionamento")]
    partial class AdicionadoAutoRelacionamento
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Tnf.Architecture.Domain.Registration.Person", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Name");

                    b.Property<int>("ParentId");

                    b.HasKey("Id");

                    b.ToTable("People");
                });

            modelBuilder.Entity("Tnf.Architecture.Domain.Registration.Person", b =>
                {
                    b.HasOne("Tnf.Architecture.Domain.Registration.Person", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("Id")
                        .HasPrincipalKey("ParentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
