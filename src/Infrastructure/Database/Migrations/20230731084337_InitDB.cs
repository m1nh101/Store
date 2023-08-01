using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class InitDB : Migration
{
  /// <inheritdoc />
  protected override void Up(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.CreateTable(
        name: "Identifier",
        columns: table => new
        {
          Id = table.Column<string>(type: "nvarchar(450)", nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Identifier", x => x.Id);
        });

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
          Status = table.Column<int>(type: "int", nullable: false),
          LastTimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Orders", x => x.Id);
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

    migrationBuilder.CreateTable(
        name: "Users",
        columns: table => new
        {
          Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
          FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
          Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
          UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
          Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
          LastTimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Users", x => x.Id);
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
          Quantity = table.Column<int>(type: "int", nullable: false),
          ProductIdId = table.Column<string>(type: "nvarchar(450)", nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_OrderItems", x => new { x.OrderId, x.Id });
          table.ForeignKey(
                    name: "FK_OrderItems_Identifier_ProductIdId",
                    column: x => x.ProductIdId,
                    principalTable: "Identifier",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
          table.ForeignKey(
                    name: "FK_OrderItems_Orders_OrderId",
                    column: x => x.OrderId,
                    principalTable: "Orders",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
        });

    migrationBuilder.CreateTable(
        name: "Products",
        columns: table => new
        {
          Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
          Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
          Brand = table.Column<string>(type: "nvarchar(218)", maxLength: 218, nullable: false),
          State = table.Column<int>(type: "int", nullable: false),
          Price = table.Column<double>(type: "float", nullable: false),
          SaleId = table.Column<string>(type: "nvarchar(450)", nullable: true),
          LastTimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Products", x => x.Id);
          table.ForeignKey(
                    name: "FK_Products_Sales_SaleId",
                    column: x => x.SaleId,
                    principalTable: "Sales",
                    principalColumn: "Id");
        });

    migrationBuilder.CreateTable(
        name: "UserClaims",
        columns: table => new
        {
          UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
          Id = table.Column<int>(type: "int", nullable: false)
                .Annotation("SqlServer:Identity", "1, 1"),
          ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: false),
          Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_UserClaims", x => new { x.UserId, x.Id });
          table.ForeignKey(
                    name: "FK_UserClaims_Users_UserId",
                    column: x => x.UserId,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
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

    migrationBuilder.CreateTable(
        name: "Items",
        columns: table => new
        {
          Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
          Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
          Size = table.Column<string>(type: "nvarchar(max)", nullable: false),
          Quantity = table.Column<int>(type: "int", nullable: false),
          AdditionPrice = table.Column<double>(type: "float", nullable: false),
          ProductId = table.Column<string>(type: "nvarchar(450)", nullable: false),
          LastTimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Items", x => x.Id);
          table.ForeignKey(
                    name: "FK_Items_Products_ProductId",
                    column: x => x.ProductId,
                    principalTable: "Products",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
        });

    migrationBuilder.CreateIndex(
        name: "IX_Items_ProductId",
        table: "Items",
        column: "ProductId");

    migrationBuilder.CreateIndex(
        name: "IX_OrderItems_ProductIdId",
        table: "OrderItems",
        column: "ProductIdId");

    migrationBuilder.CreateIndex(
        name: "IX_Products_Name_Brand",
        table: "Products",
        columns: new[] { "Name", "Brand" });

    migrationBuilder.CreateIndex(
        name: "IX_Products_SaleId",
        table: "Products",
        column: "SaleId");
  }

  /// <inheritdoc />
  protected override void Down(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.DropTable(
        name: "Images");

    migrationBuilder.DropTable(
        name: "Items");

    migrationBuilder.DropTable(
        name: "OrderItems");

    migrationBuilder.DropTable(
        name: "UserClaims");

    migrationBuilder.DropTable(
        name: "Products");

    migrationBuilder.DropTable(
        name: "Identifier");

    migrationBuilder.DropTable(
        name: "Orders");

    migrationBuilder.DropTable(
        name: "Users");

    migrationBuilder.DropTable(
        name: "Sales");
  }
}
