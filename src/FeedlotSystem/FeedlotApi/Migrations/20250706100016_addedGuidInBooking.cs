using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FeedlotApi.Migrations
{
    /// <inheritdoc />
    public partial class addedGuidInBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PublicId",
                table: "Bookings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: Guid.Empty);

            migrationBuilder.Sql(@"
                    UPDATE Bookings
                    SET PublicId = NEWID()
                    WHERE PublicId IS NULL OR PublicId = '00000000-0000-0000-0000-000000000000';
                ");
            migrationBuilder.CreateIndex(
                name: "IX_Bookings_PublicId",
                table: "Bookings",
                column: "PublicId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Bookings_PublicId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "Bookings");
        }
    }
}
