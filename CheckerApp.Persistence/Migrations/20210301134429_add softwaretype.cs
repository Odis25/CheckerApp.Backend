using Microsoft.EntityFrameworkCore.Migrations;

namespace CheckerApp.Persistence.Migrations
{
    public partial class addsoftwaretype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SoftwareType",
                table: "Softwares",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoftwareType",
                table: "Softwares");
        }
    }
}
