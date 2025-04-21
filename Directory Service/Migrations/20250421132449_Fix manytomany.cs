using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Directory_Service.Migrations
{
    /// <inheritdoc />
    public partial class Fixmanytomany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentTypes_EducationLevels_EducationLevelId",
                table: "DocumentTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_EducationLevels_DocumentTypes_DocumentTypeEntityId",
                table: "EducationLevels");

            migrationBuilder.DropIndex(
                name: "IX_EducationLevels_DocumentTypeEntityId",
                table: "EducationLevels");

            migrationBuilder.DropColumn(
                name: "DocumentTypeEntityId",
                table: "EducationLevels");

            migrationBuilder.CreateTable(
                name: "DocumentTypeNextEducationLevels",
                columns: table => new
                {
                    DocumentTypesAsNextLevelId = table.Column<Guid>(type: "uuid", nullable: false),
                    NextEducationLevelsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTypeNextEducationLevels", x => new { x.DocumentTypesAsNextLevelId, x.NextEducationLevelsId });
                    table.ForeignKey(
                        name: "FK_DocumentTypeNextEducationLevels_DocumentTypes_DocumentTypes~",
                        column: x => x.DocumentTypesAsNextLevelId,
                        principalTable: "DocumentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentTypeNextEducationLevels_EducationLevels_NextEducati~",
                        column: x => x.NextEducationLevelsId,
                        principalTable: "EducationLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTypeNextEducationLevels_NextEducationLevelsId",
                table: "DocumentTypeNextEducationLevels",
                column: "NextEducationLevelsId");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentTypes_EducationLevels_EducationLevelId",
                table: "DocumentTypes",
                column: "EducationLevelId",
                principalTable: "EducationLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentTypes_EducationLevels_EducationLevelId",
                table: "DocumentTypes");

            migrationBuilder.DropTable(
                name: "DocumentTypeNextEducationLevels");

            migrationBuilder.AddColumn<Guid>(
                name: "DocumentTypeEntityId",
                table: "EducationLevels",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EducationLevels_DocumentTypeEntityId",
                table: "EducationLevels",
                column: "DocumentTypeEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentTypes_EducationLevels_EducationLevelId",
                table: "DocumentTypes",
                column: "EducationLevelId",
                principalTable: "EducationLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EducationLevels_DocumentTypes_DocumentTypeEntityId",
                table: "EducationLevels",
                column: "DocumentTypeEntityId",
                principalTable: "DocumentTypes",
                principalColumn: "Id");
        }
    }
}
