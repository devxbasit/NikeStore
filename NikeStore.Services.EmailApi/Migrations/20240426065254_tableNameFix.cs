using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NikeStore.Services.EmailApi.Migrations
{
    /// <inheritdoc />
    public partial class tableNameFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailLogger",
                table: "EmailLogger");

            migrationBuilder.RenameTable(
                name: "EmailLogger",
                newName: "EmailLoggers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmailLoggers",
                table: "EmailLoggers",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailLoggers",
                table: "EmailLoggers");

            migrationBuilder.RenameTable(
                name: "EmailLoggers",
                newName: "EmailLogger");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmailLogger",
                table: "EmailLogger",
                column: "Id");
        }
    }
}
