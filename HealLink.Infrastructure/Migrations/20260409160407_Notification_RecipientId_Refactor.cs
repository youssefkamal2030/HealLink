using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealLink.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Notification_RecipientId_Refactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Doctors_DoctorId1",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Patients_PatientId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_DoctorId1",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_PatientId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "DoctorId1",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "Notifications");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Subscriptions",
                newName: "Amount_Amount");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Payments",
                newName: "Amount_Amount");

            migrationBuilder.AddColumn<Guid>(
                name: "PatientId1",
                table: "TestResults",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount_Amount",
                table: "Subscriptions",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Amount_Currency",
                table: "Subscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount_Amount",
                table: "Payments",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Amount_Currency",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "OTPs",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "OTPs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                table: "OTPs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "OTPs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Notifications",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "RecipientId",
                table: "Notifications",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PatientId1",
                table: "MedicationReminders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PrescriptionId1",
                table: "MedicationReminders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestResults_PatientId1",
                table: "TestResults",
                column: "PatientId1");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_RecipientId_RecipientType",
                table: "Notifications",
                columns: new[] { "RecipientId", "RecipientType" });

            migrationBuilder.CreateIndex(
                name: "IX_MedicationReminders_PatientId1",
                table: "MedicationReminders",
                column: "PatientId1");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationReminders_PrescriptionId1",
                table: "MedicationReminders",
                column: "PrescriptionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicationReminders_Patients_PatientId1",
                table: "MedicationReminders",
                column: "PatientId1",
                principalTable: "Patients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicationReminders_Prescriptions_PrescriptionId1",
                table: "MedicationReminders",
                column: "PrescriptionId1",
                principalTable: "Prescriptions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TestResults_Patients_PatientId1",
                table: "TestResults",
                column: "PatientId1",
                principalTable: "Patients",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicationReminders_Patients_PatientId1",
                table: "MedicationReminders");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicationReminders_Prescriptions_PrescriptionId1",
                table: "MedicationReminders");

            migrationBuilder.DropForeignKey(
                name: "FK_TestResults_Patients_PatientId1",
                table: "TestResults");

            migrationBuilder.DropIndex(
                name: "IX_TestResults_PatientId1",
                table: "TestResults");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_RecipientId_RecipientType",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_MedicationReminders_PatientId1",
                table: "MedicationReminders");

            migrationBuilder.DropIndex(
                name: "IX_MedicationReminders_PrescriptionId1",
                table: "MedicationReminders");

            migrationBuilder.DropColumn(
                name: "PatientId1",
                table: "TestResults");

            migrationBuilder.DropColumn(
                name: "Amount_Currency",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "Amount_Currency",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "OTPs");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                table: "OTPs");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "OTPs");

            migrationBuilder.DropColumn(
                name: "RecipientId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "PatientId1",
                table: "MedicationReminders");

            migrationBuilder.DropColumn(
                name: "PrescriptionId1",
                table: "MedicationReminders");

            migrationBuilder.RenameColumn(
                name: "Amount_Amount",
                table: "Subscriptions",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "Amount_Amount",
                table: "Payments",
                newName: "Amount");

            migrationBuilder.AlterColumn<int>(
                name: "Amount",
                table: "Subscriptions",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "Amount",
                table: "Payments",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "OTPs",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<Guid>(
                name: "DoctorId1",
                table: "Notifications",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PatientId",
                table: "Notifications",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_DoctorId1",
                table: "Notifications",
                column: "DoctorId1");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_PatientId",
                table: "Notifications",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Doctors_DoctorId1",
                table: "Notifications",
                column: "DoctorId1",
                principalTable: "Doctors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Patients_PatientId",
                table: "Notifications",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id");
        }
    }
}
