using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Predictr.Data.Migrations
{
    public partial class fixtureisforeignkeyonprediction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Predictions_FixtureId",
                table: "Predictions",
                column: "FixtureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Predictions_Fixtures_FixtureId",
                table: "Predictions",
                column: "FixtureId",
                principalTable: "Fixtures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Predictions_Fixtures_FixtureId",
                table: "Predictions");

            migrationBuilder.DropIndex(
                name: "IX_Predictions_FixtureId",
                table: "Predictions");
        }
    }
}
