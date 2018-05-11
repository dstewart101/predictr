using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Predictr.Data.Migrations
{
    public partial class users : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ApplicationUser",
                table: "Predictions",
                newName: "ApplicationUserId");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Predictions",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Predictions_ApplicationUserId",
                table: "Predictions",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Predictions_AspNetUsers_ApplicationUserId",
                table: "Predictions",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Predictions_AspNetUsers_ApplicationUserId",
                table: "Predictions");

            migrationBuilder.DropIndex(
                name: "IX_Predictions_ApplicationUserId",
                table: "Predictions");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Predictions",
                newName: "ApplicationUser");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUser",
                table: "Predictions",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
