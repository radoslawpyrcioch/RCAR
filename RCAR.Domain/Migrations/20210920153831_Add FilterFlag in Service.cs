using Microsoft.EntityFrameworkCore.Migrations;

namespace RCAR.Domain.Migrations
{
    public partial class AddFilterFlaginService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Filter",
                table: "Services",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Filter",
                table: "Services");
        }
    }
}
