using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SaleMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SaleIdId",
                table: "Products",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Identitifer",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identitifer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Value = table.Column<short>(type: "smallint", nullable: false),
                    LastTimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_SaleIdId",
                table: "Products",
                column: "SaleIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Identitifer_SaleIdId",
                table: "Products",
                column: "SaleIdId",
                principalTable: "Identitifer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Sales_Id",
                table: "Products",
                column: "Id",
                principalTable: "Sales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Identitifer_SaleIdId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Sales_Id",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Identitifer");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Products_SaleIdId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SaleIdId",
                table: "Products");
        }
    }
}
