using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Doctrim.EF.Migrations
{
    public partial class updatedMapping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "FileByteArray",
                table: "Documents",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileByteArray",
                table: "Documents");
        }
    }
}
