using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class UsersData2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersDatas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsersDatas",
                columns: table => new
                {
                    UsersDataId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersDatas", x => x.UsersDataId);
                });
        }
    }
}
