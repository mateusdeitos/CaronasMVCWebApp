using Microsoft.EntityFrameworkCore.Migrations;

namespace CaronasMVCWebApp.Migrations
{
    public partial class Rides_cost_attribute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Custo",
                table: "Rides",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Custo",
                table: "Rides");
        }
    }
}
