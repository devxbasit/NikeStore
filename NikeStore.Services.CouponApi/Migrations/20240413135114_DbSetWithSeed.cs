using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NikeStore.Services.CouponApi.Migrations
{
    /// <inheritdoc />
    public partial class DbSetWithSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coup",
                columns: table => new
                {
                    CouponId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CouponCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiscountAmount = table.Column<int>(type: "int", nullable: false),
                    MinAmount = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coup", x => x.CouponId);
                });

            migrationBuilder.InsertData(
                table: "Coup",
                columns: new[] { "CouponId", "CouponCode", "CreatedDateTime", "DiscountAmount", "LastUpdatedDateTime", "MinAmount" },
                values: new object[,]
                {
                    { 1, "GUEST_100_OFF", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1500 },
                    { 2, "GUEST_200_OFF", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 200, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2500 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coup");
        }
    }
}
