using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace User_Service.Migrations
{
    /// <inheritdoc />
    public partial class MovedpasswordHashintoUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "HeadManagers");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Applicants");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Administrators");

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Managers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "HeadManagers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Applicants",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Administrators",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
