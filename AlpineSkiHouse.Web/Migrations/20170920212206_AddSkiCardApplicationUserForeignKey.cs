using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AlpineSkiHouse.Web.Migrations
{
    public partial class AddSkiCardApplicationUserForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey("FK_SkiCards_ApplicationUser_ApplicationUserID", "SkiCards", "ApplicationUserId",
                "AspNetUsers", principalColumn: "Id", onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey("FK_SkiCards_ApplicationUser_ApplicationUserID", "SkiCards");
        }
    }
}
