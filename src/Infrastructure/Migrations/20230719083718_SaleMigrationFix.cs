using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SaleMigrationFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Identitifer_SaleIdId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Sales_Id",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Identitifer");

            migrationBuilder.RenameColumn(
                name: "SaleIdId",
                table: "Products",
                newName: "SaleId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_SaleIdId",
                table: "Products",
                newName: "IX_Products_SaleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Sales_SaleId",
                table: "Products",
                column: "SaleId",
                principalTable: "Sales",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Sales_SaleId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "SaleId",
                table: "Products",
                newName: "SaleIdId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_SaleId",
                table: "Products",
                newName: "IX_Products_SaleIdId");

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
    }
}
