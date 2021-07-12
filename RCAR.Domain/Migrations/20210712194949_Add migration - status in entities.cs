using Microsoft.EntityFrameworkCore.Migrations;

namespace RCAR.Domain.Migrations
{
    public partial class Addmigrationstatusinentities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Services",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Cars",
                newName: "Status");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "PaymentRecords",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "PaymentRecords");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Services",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Cars",
                newName: "ImageUrl");
        }
    }
}
