using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealLink.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixOtpUserId1ShadowProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OTPs_Users_UserId1",
                table: "OTPs");

            migrationBuilder.DropIndex(
                name: "IX_OTPs_UserId1",
                table: "OTPs");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "OTPs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "OTPs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OTPs_UserId1",
                table: "OTPs",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_OTPs_Users_UserId1",
                table: "OTPs",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
