using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace User_Service.Migrations
{
    /// <inheritdoc />
    public partial class MovedrefreshtokenintoUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryDate",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "HeadManagers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryDate",
                table: "HeadManagers");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Applicants");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryDate",
                table: "Applicants");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Administrators");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryDate",
                table: "Administrators");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryDate",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryDate",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Managers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryDate",
                table: "Managers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "HeadManagers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryDate",
                table: "HeadManagers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Applicants",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryDate",
                table: "Applicants",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Administrators",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryDate",
                table: "Administrators",
                type: "timestamp with time zone",
                nullable: true);
        }
    }
}
