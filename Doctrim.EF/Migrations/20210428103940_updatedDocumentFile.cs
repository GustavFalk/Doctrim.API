using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Doctrim.EF.Migrations
{
    public partial class updatedDocumentFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileByteArray",
                table: "Documents");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "FileByteArray",
                table: "Documents",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
