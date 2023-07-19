using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations;

/// <inheritdoc />
public partial class CreateDatabase : Migration
{
  /// <inheritdoc />
  protected override void Up(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.CreateTable(
        name: "Products",
        columns: table => new
        {
          Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
          Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
          Brand = table.Column<string>(type: "nvarchar(218)", maxLength: 218, nullable: false),
          Price = table.Column<double>(type: "float", nullable: false),
          State = table.Column<int>(type: "int", nullable: false),
          LastTimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Products", x => x.Id);
        });

    migrationBuilder.CreateTable(
        name: "Images",
        columns: table => new
        {
          ProductId = table.Column<string>(type: "nvarchar(450)", nullable: false),
          Id = table.Column<int>(type: "int", nullable: false)
                .Annotation("SqlServer:Identity", "1, 1"),
          Url = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Images", x => new { x.ProductId, x.Id });
          table.ForeignKey(
                    name: "FK_Images_Products_ProductId",
                    column: x => x.ProductId,
                    principalTable: "Products",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
        });

    migrationBuilder.CreateIndex(
        name: "IX_Products_Name_Brand",
        table: "Products",
        columns: new[] { "Name", "Brand" });
  }

  /// <inheritdoc />
  protected override void Down(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.DropTable(
        name: "Images");

    migrationBuilder.DropTable(
        name: "Products");
  }
}
