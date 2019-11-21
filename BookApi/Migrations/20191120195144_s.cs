using Microsoft.EntityFrameworkCore.Migrations;

namespace BookApi.Migrations
{
    public partial class s : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "a",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "b",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "c",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "d",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "e",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "f",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "g",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "h",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "i",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "j",
                table: "Employees",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "a",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "b",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "c",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "d",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "e",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "f",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "g",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "h",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "i",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "j",
                table: "Employees");
        }
    }
}
