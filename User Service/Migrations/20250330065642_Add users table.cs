using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace User_Service.Migrations
{
    /// <inheritdoc />
    public partial class Adduserstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserRole = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Administrators_Users_Id",
                table: "Administrators",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Applicants_Users_Id",
                table: "Applicants",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HeadManagers_Users_Id",
                table: "HeadManagers",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_Users_Id",
                table: "Managers",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Administrators_Users_Id",
                table: "Administrators");

            migrationBuilder.DropForeignKey(
                name: "FK_Applicants_Users_Id",
                table: "Applicants");

            migrationBuilder.DropForeignKey(
                name: "FK_HeadManagers_Users_Id",
                table: "HeadManagers");

            migrationBuilder.DropForeignKey(
                name: "FK_Managers_Users_Id",
                table: "Managers");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
