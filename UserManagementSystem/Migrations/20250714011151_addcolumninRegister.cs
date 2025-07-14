using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class addcolumninRegister : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Registers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Registers");
        }
    }
}
