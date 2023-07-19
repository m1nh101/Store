using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations;

/// <inheritdoc />
public partial class AddUserMigration : Migration
{
  /// <inheritdoc />
  protected override void Up(MigrationBuilder migrationBuilder)
  {
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
  }

  /// <inheritdoc />
  protected override void Down(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.DropTable(
        name: "UserClaims");

    migrationBuilder.DropTable(
        name: "Users");
  }
}
