using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NikeStore.Services.CouponApi.Migrations
{
    /// <inheritdoc />
    public partial class dbsetUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Coup",
                table: "Coup");

            migrationBuilder.RenameTable(
                name: "Coup",
                newName: "Coupons");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Coupons",
                table: "Coupons",
                column: "CouponId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Coupons",
                table: "Coupons");

            migrationBuilder.RenameTable(
                name: "Coupons",
                newName: "Coup");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Coup",
                table: "Coup",
                column: "CouponId");
        }
    }
}
