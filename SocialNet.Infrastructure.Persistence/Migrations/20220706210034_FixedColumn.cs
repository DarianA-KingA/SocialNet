using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialNet.Infrastructure.Persistence.Migrations
{
    public partial class FixedColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Pasword",
                table: "Users",
                newName: "Password");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Friends",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Friends");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "Pasword");
        }
    }
}
