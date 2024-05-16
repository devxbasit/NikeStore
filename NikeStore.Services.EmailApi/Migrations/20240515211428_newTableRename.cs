using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NikeStore.Services.EmailApi.Migrations
{
    /// <inheritdoc />
    public partial class newTableRename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MailMessages",
                table: "MailMessages");

            migrationBuilder.RenameTable(
                name: "MailMessages",
                newName: "DbMailLogs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DbMailLogs",
                table: "DbMailLogs",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DbMailLogs",
                table: "DbMailLogs");

            migrationBuilder.RenameTable(
                name: "DbMailLogs",
                newName: "MailMessages");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MailMessages",
                table: "MailMessages",
                column: "Id");
        }
    }
}
