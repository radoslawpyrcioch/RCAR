using Microsoft.EntityFrameworkCore.Migrations;

namespace RCAR.Domain.Migrations
{
    public partial class AddmigrationAddFlagIsRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Services");

            migrationBuilder.RenameColumn(
                name: "MiddleName",
                table: "Services",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "MiddleName",
                table: "Members",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Members",
                newName: "Email");

            migrationBuilder.AddColumn<bool>(
                name: "IsRemoved",
                table: "Services",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRemoved",
                table: "PaymentRecords",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRemoved",
                table: "Members",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRemoved",
                table: "Cars",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRemoved",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "IsRemoved",
                table: "PaymentRecords");

            migrationBuilder.DropColumn(
                name: "IsRemoved",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "IsRemoved",
                table: "Cars");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Services",
                newName: "MiddleName");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Members",
                newName: "MiddleName");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Members",
                newName: "FullName");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Services",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
