using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Predictr.Data.Migrations
{
    public partial class simplifyfixture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fixtures_Teams_TeamId",
                table: "Fixtures");

            migrationBuilder.DropIndex(
                name: "IX_Fixtures_TeamId",
                table: "Fixtures");

            migrationBuilder.DropColumn(
                name: "AwayTeamId",
                table: "Fixtures");

            migrationBuilder.DropColumn(
                name: "HomeTeamId",
                table: "Fixtures");

            migrationBuilder.DropColumn(
                name: "ResultId",
                table: "Fixtures");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Fixtures");

            migrationBuilder.AlterColumn<int>(
                name: "HomeScore",
                table: "Fixtures",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "AwayTeamScore",
                table: "Fixtures",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "Away",
                table: "Fixtures",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Home",
                table: "Fixtures",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Result",
                table: "Fixtures",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Away",
                table: "Fixtures");

            migrationBuilder.DropColumn(
                name: "Home",
                table: "Fixtures");

            migrationBuilder.DropColumn(
                name: "Result",
                table: "Fixtures");

            migrationBuilder.AlterColumn<int>(
                name: "HomeScore",
                table: "Fixtures",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AwayTeamScore",
                table: "Fixtures",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AwayTeamId",
                table: "Fixtures",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HomeTeamId",
                table: "Fixtures",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ResultId",
                table: "Fixtures",
                nullable: false,
                defaultValue: 0);

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
    }
}
