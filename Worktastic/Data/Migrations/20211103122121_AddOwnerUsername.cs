using Microsoft.EntityFrameworkCore.Migrations;

namespace Worktastic.Data.Migrations
{
    public partial class AddOwnerUsername : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "startDate",
                table: "Jobs",
                newName: "StartDate");

            migrationBuilder.AddColumn<string>(
                name: "OwnerUsername",
                table: "Jobs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerUsername",
                table: "Jobs");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Jobs",
                newName: "startDate");
        }
    }
}
