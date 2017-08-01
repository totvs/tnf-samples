using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Tnf.Architecture.EntityFrameworkCore.Contexts;

namespace Tnf.Architecture.EntityFrameworkCore.Migrations
{
    [DbContext(typeof(LegacyDbContext))]
    [Migration("20170801141420_Initialize")]
    partial class Initialize
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Tnf.Architecture.EntityFrameworkCore.Entities.ProfessionalPoco", b =>
                {
                    b.Property<decimal>("ProfessionalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("SYS009_PROFESSIONAL_ID")
                        .HasDefaultValueSql("SELECT ISNULL(MAX(SYS009_PROFESSIONAL_ID), 1) FROM SYS009_PROFESSIONAL");

                    b.Property<Guid>("Code")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("SYS009_PROFESSIONAL_CODE");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnName("SYS009_ADDRESS")
                        .HasMaxLength(50);

                    b.Property<string>("AddressComplement")
                        .IsRequired()
                        .HasColumnName("SYS009_ADDRESS_COMPLEMENT")
                        .HasMaxLength(100);

                    b.Property<string>("AddressNumber")
                        .IsRequired()
                        .HasColumnName("SYS009_ADDRESS_NUMBER")
                        .HasMaxLength(9);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("SYS009_EMAIL")
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("SYS009_NAME")
                        .HasMaxLength(50);

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnName("SYS009_PHONE")
                        .HasMaxLength(50);

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnName("SYS009_ZIP_CODE")
                        .HasMaxLength(15);

                    b.HasKey("ProfessionalId", "Code");

                    b.ToTable("SYS009_PROFESSIONAL");
                });

            modelBuilder.Entity("Tnf.Architecture.EntityFrameworkCore.Entities.ProfessionalSpecialtiesPoco", b =>
                {
                    b.Property<decimal>("ProfessionalId")
                        .HasColumnName("SYS009_PROFESSIONAL_ID");

                    b.Property<Guid>("Code")
                        .HasColumnName("SYS009_PROFESSIONAL_CODE");

                    b.Property<int>("SpecialtyId")
                        .HasColumnName("SYS011_SPECIALTIES_ID");

                    b.HasKey("ProfessionalId", "Code", "SpecialtyId");

                    b.HasIndex("SpecialtyId");

                    b.ToTable("SYS010_PROFESSIONAL_SPECIALTIES");
                });

            modelBuilder.Entity("Tnf.Architecture.EntityFrameworkCore.Entities.SpecialtyPoco", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("SYS011_SPECIALTIES_ID");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("SYS011_SPECIALTIES_DESCRIPTION")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("SYS011_SPECIALTIES");
                });

            modelBuilder.Entity("Tnf.Architecture.EntityFrameworkCore.Entities.ProfessionalSpecialtiesPoco", b =>
                {
                    b.HasOne("Tnf.Architecture.EntityFrameworkCore.Entities.SpecialtyPoco", "Specialty")
                        .WithMany("ProfessionalSpecialties")
                        .HasForeignKey("SpecialtyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Tnf.Architecture.EntityFrameworkCore.Entities.ProfessionalPoco", "Professional")
                        .WithMany("ProfessionalSpecialties")
                        .HasForeignKey("ProfessionalId", "Code")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
