using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealLink.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OTPUSERCONFIGURATIONS : Migration
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

            migrationBuilder.AlterColumn<bool>(
                name: "IsUsed",
                table: "OTPs",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "OTPs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsUsed",
                table: "OTPs",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "OTPs",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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
