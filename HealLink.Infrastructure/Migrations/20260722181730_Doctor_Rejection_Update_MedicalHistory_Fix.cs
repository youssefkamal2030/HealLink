using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealLink.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Doctor_Rejection_Update_MedicalHistory_Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MedicalHistories_PatientId",
                table: "MedicalHistories");

            migrationBuilder.AddColumn<DateTime>(
                name: "RejectionDate",
                table: "Doctors",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RejectionReason",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Rejection_RejectedBy",
                table: "Doctors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedicalHistories_PatientId",
                table: "MedicalHistories",
                column: "PatientId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MedicalHistories_PatientId",
                table: "MedicalHistories");

            migrationBuilder.DropColumn(
                name: "RejectionDate",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "RejectionReason",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "Rejection_RejectedBy",
                table: "Doctors");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalHistories_PatientId",
                table: "MedicalHistories",
                column: "PatientId");
        }
    }
}
