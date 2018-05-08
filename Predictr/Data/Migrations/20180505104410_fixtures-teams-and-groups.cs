using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Predictr.Data.Migrations
{
    public partial class fixturesteamsandgroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Group",
                table: "Fixtures",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Fixtures",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fixtures_TeamId",
                table: "Fixtures",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fixtures_Teams_TeamId",
                table: "Fixtures",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fixtures_Teams_TeamId",
                table: "Fixtures");

            migrationBuilder.DropIndex(
                name: "IX_Fixtures_TeamId",
                table: "Fixtures");

            migrationBuilder.DropColumn(
                name: "Group",
                table: "Fixtures");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Fixtures");
        }
    }
}
