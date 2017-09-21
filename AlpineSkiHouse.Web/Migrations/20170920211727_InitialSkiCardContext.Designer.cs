using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using AlpineSkiHouse.Web.Data;

namespace AlpineSkiHouse.Web.Migrations
{
    [DbContext(typeof(SkiCardContext))]
    [Migration("20170920211727_InitialSkiCardContext")]
    partial class InitialSkiCardContext
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AlpineSkiHouse.Web.Models.SkiCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId");

                    b.Property<DateTime>("CardHolderBirthDate");

                    b.Property<string>("CardHolderFirstName")
                        .IsRequired()
                        .HasMaxLength(70);

                    b.Property<string>("CardHolderLastName")
                        .HasMaxLength(70);

                    b.Property<string>("CardHolderPhoneNumber");

                    b.Property<DateTime>("CreatedOn");

                    b.HasKey("Id");

                    b.ToTable("SkiCards");
                });
        }
    }
}
