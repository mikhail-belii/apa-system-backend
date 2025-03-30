using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace User_Service.Migrations
{
    /// <inheritdoc />
    public partial class MovedemailintoUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "HeadManagers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Applicants");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Administrators");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Managers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "HeadManagers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Applicants",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Administrators",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
