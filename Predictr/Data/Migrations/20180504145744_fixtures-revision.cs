using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Predictr.Data.Migrations
{
    public partial class fixturesrevision : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Fixture",
                table: "Fixture");

            migrationBuilder.RenameTable(
                name: "Fixture",
                newName: "Fixtures");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Fixtures",
                table: "Fixtures",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Fixtures",
                table: "Fixtures");

            migrationBuilder.RenameTable(
                name: "Fixtures",
                newName: "Fixture");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Fixture",
                table: "Fixture",
                column: "Id");
        }
    }
}
