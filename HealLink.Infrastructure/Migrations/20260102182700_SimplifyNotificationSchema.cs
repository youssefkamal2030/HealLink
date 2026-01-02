using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealLink.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SimplifyNotificationSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConnectionId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "RelatedDoctorId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "RelatedPatientId",
                table: "Notifications");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ConnectionId",
                table: "Notifications",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RelatedDoctorId",
                table: "Notifications",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RelatedPatientId",
                table: "Notifications",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
