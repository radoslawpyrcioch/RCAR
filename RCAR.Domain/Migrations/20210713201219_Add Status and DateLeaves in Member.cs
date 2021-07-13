using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RCAR.Domain.Migrations
{
    public partial class AddStatusandDateLeavesinMember : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateLeaves",
                table: "Members",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Members",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateLeaves",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Members");
        }
    }
}
