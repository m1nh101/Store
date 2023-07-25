using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations;

/// <inheritdoc />
public partial class OrderTable : Migration
{
  /// <inheritdoc />
  protected override void Up(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.CreateTable(
        name: "Orders",
        columns: table => new
        {
          Id = table.Column<int>(type: "int", nullable: false)
                .Annotation("SqlServer:Identity", "1, 1"),
          Address_Address1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
          Address_Address2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
          Address_PostCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
          PaidTime = table.Column<DateTime>(type: "datetime2", nullable: false),
          UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
          LastTimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Orders", x => x.Id);
        });

    migrationBuilder.CreateTable(
        name: "OrderItems",
        columns: table => new
        {
          OrderId = table.Column<int>(type: "int", nullable: false),
          Id = table.Column<int>(type: "int", nullable: false)
                .Annotation("SqlServer:Identity", "1, 1"),
          Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
          Price = table.Column<double>(type: "float", nullable: false),
          Quantity = table.Column<int>(type: "int", nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_OrderItems", x => new { x.OrderId, x.Id });
          table.ForeignKey(
                    name: "FK_OrderItems_Orders_OrderId",
                    column: x => x.OrderId,
                    principalTable: "Orders",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
        });
  }

  /// <inheritdoc />
  protected override void Down(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.DropTable(
        name: "OrderItems");

    migrationBuilder.DropTable(
        name: "Orders");
  }
}
