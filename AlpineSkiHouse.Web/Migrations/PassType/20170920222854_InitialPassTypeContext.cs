using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AlpineSkiHouse.Web.Migrations.PassType
{
    public partial class InitialPassTypeContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PassTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    MaxActivations = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    ValidFrom = table.Column<DateTime>(nullable: false),
                    ValidTo = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PassTypePrice",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MaxAge = table.Column<int>(nullable: false),
                    MinAge = table.Column<int>(nullable: false),
                    PassTypeId = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassTypePrice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PassTypePrice_PassTypes_PassTypeId",
                        column: x => x.PassTypeId,
                        principalTable: "PassTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PassTypeResort",
                columns: table => new
                {
                    PassTypeId = table.Column<int>(nullable: false),
                    ResordId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassTypeResort", x => new { x.PassTypeId, x.ResordId });
                    table.ForeignKey(
                        name: "FK_PassTypeResort_PassTypes_PassTypeId",
                        column: x => x.PassTypeId,
                        principalTable: "PassTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PassTypePrice_PassTypeId",
                table: "PassTypePrice",
                column: "PassTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PassTypePrice");

            migrationBuilder.DropTable(
                name: "PassTypeResort");

            migrationBuilder.DropTable(
                name: "PassTypes");
        }
    }
}
