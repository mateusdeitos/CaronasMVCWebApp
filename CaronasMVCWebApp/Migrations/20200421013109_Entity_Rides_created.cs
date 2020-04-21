using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CaronasMVCWebApp.Migrations
{
    public partial class Entity_Rides_created : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RidesId",
                table: "Members",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Rides",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Data = table.Column<DateTime>(nullable: false),
                    DestinoId = table.Column<int>(nullable: true),
                    MotoristaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rides", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rides_Destinies_DestinoId",
                        column: x => x.DestinoId,
                        principalTable: "Destinies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rides_Members_MotoristaId",
                        column: x => x.MotoristaId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Members_RidesId",
                table: "Members",
                column: "RidesId");

            migrationBuilder.CreateIndex(
                name: "IX_Rides_DestinoId",
                table: "Rides",
                column: "DestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_Rides_MotoristaId",
                table: "Rides",
                column: "MotoristaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Rides_RidesId",
                table: "Members",
                column: "RidesId",
                principalTable: "Rides",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Rides_RidesId",
                table: "Members");

            migrationBuilder.DropTable(
                name: "Rides");

            migrationBuilder.DropIndex(
                name: "IX_Members_RidesId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "RidesId",
                table: "Members");
        }
    }
}
