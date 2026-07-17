using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealLink.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class subscription_and_stuff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicationReminders_Prescriptions_PrescriptionId",
                table: "MedicationReminders");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicationReminders_Prescriptions_PrescriptionId1",
                table: "MedicationReminders");

            migrationBuilder.DropIndex(
                name: "IX_MedicationReminders_PrescriptionId1",
                table: "MedicationReminders");

            migrationBuilder.DropColumn(
                name: "PrescriptionId1",
                table: "MedicationReminders");

            migrationBuilder.RenameColumn(
                name: "PaymentMethod",
                table: "Payments",
                newName: "Details_PaymentMethod");

            migrationBuilder.RenameColumn(
                name: "Amount_Currency",
                table: "Payments",
                newName: "Details_Currency");

            migrationBuilder.RenameColumn(
                name: "Amount_Amount",
                table: "Payments",
                newName: "Details_Amount");

            migrationBuilder.AddColumn<string>(
                name: "Details_Allergies",
                table: "MedicalHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Details_ChronicConditions",
                table: "MedicalHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Details_CurrentMedications",
                table: "MedicalHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Details_FamilyHistory",
                table: "MedicalHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Details_Notes",
                table: "MedicalHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Details_PreviousSurgeries",
                table: "MedicalHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address_City",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Country",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicationReminders_Prescriptions_PrescriptionId",
                table: "MedicationReminders",
                column: "PrescriptionId",
                principalTable: "Prescriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicationReminders_Prescriptions_PrescriptionId",
                table: "MedicationReminders");

            migrationBuilder.DropColumn(
                name: "Details_Allergies",
                table: "MedicalHistories");

            migrationBuilder.DropColumn(
                name: "Details_ChronicConditions",
                table: "MedicalHistories");

            migrationBuilder.DropColumn(
                name: "Details_CurrentMedications",
                table: "MedicalHistories");

            migrationBuilder.DropColumn(
                name: "Details_FamilyHistory",
                table: "MedicalHistories");

            migrationBuilder.DropColumn(
                name: "Details_Notes",
                table: "MedicalHistories");

            migrationBuilder.DropColumn(
                name: "Details_PreviousSurgeries",
                table: "MedicalHistories");

            migrationBuilder.DropColumn(
                name: "Address_City",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "Address_Country",
                table: "Doctors");

            migrationBuilder.RenameColumn(
                name: "Details_PaymentMethod",
                table: "Payments",
                newName: "PaymentMethod");

            migrationBuilder.RenameColumn(
                name: "Details_Currency",
                table: "Payments",
                newName: "Amount_Currency");

            migrationBuilder.RenameColumn(
                name: "Details_Amount",
                table: "Payments",
                newName: "Amount_Amount");

            migrationBuilder.AddColumn<Guid>(
                name: "PrescriptionId1",
                table: "MedicationReminders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedicationReminders_PrescriptionId1",
                table: "MedicationReminders",
                column: "PrescriptionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicationReminders_Prescriptions_PrescriptionId",
                table: "MedicationReminders",
                column: "PrescriptionId",
                principalTable: "Prescriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicationReminders_Prescriptions_PrescriptionId1",
                table: "MedicationReminders",
                column: "PrescriptionId1",
                principalTable: "Prescriptions",
                principalColumn: "Id");
        }
    }
}
