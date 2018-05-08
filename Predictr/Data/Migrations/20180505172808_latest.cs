using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Predictr.Data.Migrations
{
    public partial class latest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Prediction",
                table: "Prediction");

            migrationBuilder.RenameTable(
                name: "Prediction",
                newName: "Predictions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Predictions",
                table: "Predictions",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Predictions",
                table: "Predictions");

            migrationBuilder.RenameTable(
                name: "Predictions",
                newName: "Prediction");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prediction",
                table: "Prediction",
                column: "Id");
        }
    }
}
