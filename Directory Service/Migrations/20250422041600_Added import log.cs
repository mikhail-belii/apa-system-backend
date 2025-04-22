using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Directory_Service.Migrations
{
    /// <inheritdoc />
    public partial class Addedimportlog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DirectoryImportLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DirectoryType = table.Column<string>(type: "text", nullable: false),
                    ImportTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RecordsCount = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectoryImportLogs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DirectoryImportLogs");
        }
    }
}
