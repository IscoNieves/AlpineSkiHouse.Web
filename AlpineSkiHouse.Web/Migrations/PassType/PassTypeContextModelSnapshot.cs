using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using AlpineSkiHouse.Web.Data;

namespace AlpineSkiHouse.Web.Migrations.PassType
{
    [DbContext(typeof(PassTypeContext))]
    partial class PassTypeContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AlpineSkiHouse.Web.Models.PassType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<int>("MaxActivations");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<DateTime>("ValidFrom");

                    b.Property<DateTime>("ValidTo");

                    b.HasKey("Id");

                    b.ToTable("PassTypes");
                });

            modelBuilder.Entity("AlpineSkiHouse.Web.Models.PassTypePrice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MaxAge");

                    b.Property<int>("MinAge");

                    b.Property<int>("PassTypeId");

                    b.Property<decimal>("Price");

                    b.HasKey("Id");

                    b.HasIndex("PassTypeId");

                    b.ToTable("PassTypePrice");
                });

            modelBuilder.Entity("AlpineSkiHouse.Web.Models.PassTypeResort", b =>
                {
                    b.Property<int>("PassTypeId");

                    b.Property<int>("ResordId");

                    b.HasKey("PassTypeId", "ResordId");

                    b.ToTable("PassTypeResort");
                });

            modelBuilder.Entity("AlpineSkiHouse.Web.Models.PassTypePrice", b =>
                {
                    b.HasOne("AlpineSkiHouse.Web.Models.PassType")
                        .WithMany("Prices")
                        .HasForeignKey("PassTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AlpineSkiHouse.Web.Models.PassTypeResort", b =>
                {
                    b.HasOne("AlpineSkiHouse.Web.Models.PassType")
                        .WithMany("PassTypeResorts")
                        .HasForeignKey("PassTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
