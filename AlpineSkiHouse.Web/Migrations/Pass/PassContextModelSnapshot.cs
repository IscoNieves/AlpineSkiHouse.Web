using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using AlpineSkiHouse.Web.Data;

namespace AlpineSkiHouse.Web.Migrations.Pass
{
    [DbContext(typeof(PassContext))]
    partial class PassContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AlpineSkiHouse.Web.Models.Pass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CardId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<int>("PassTypeId");

                    b.HasKey("Id");

                    b.ToTable("Passes");
                });

            modelBuilder.Entity("AlpineSkiHouse.Web.Models.PassActivation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("PassId");

                    b.Property<int>("ScanId");

                    b.HasKey("Id");

                    b.HasIndex("PassId");

                    b.HasIndex("ScanId");

                    b.ToTable("PassActivations");
                });

            modelBuilder.Entity("AlpineSkiHouse.Web.Models.Scan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CardId");

                    b.Property<DateTime>("DateTime");

                    b.Property<int>("LocationId");

                    b.HasKey("Id");

                    b.ToTable("Scans");
                });

            modelBuilder.Entity("AlpineSkiHouse.Web.Models.PassActivation", b =>
                {
                    b.HasOne("AlpineSkiHouse.Web.Models.Pass")
                        .WithMany("Activations")
                        .HasForeignKey("PassId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AlpineSkiHouse.Web.Models.Scan", "Scan")
                        .WithMany()
                        .HasForeignKey("ScanId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
