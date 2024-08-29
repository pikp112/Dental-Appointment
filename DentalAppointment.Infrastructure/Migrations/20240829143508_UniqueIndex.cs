using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentalAppointment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UniqueIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AppointmentDate",
                table: "Appointments",
                newName: "AppointmentDateTime");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_AppointmentDateTime",
                table: "Appointments",
                column: "AppointmentDateTime",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_AppointmentDateTime_PatientPhoneNumber",
                table: "Appointments",
                columns: new[] { "AppointmentDateTime", "PatientPhoneNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientPhoneNumber",
                table: "Appointments",
                column: "PatientPhoneNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Appointments_AppointmentDateTime",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_AppointmentDateTime_PatientPhoneNumber",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_PatientPhoneNumber",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "AppointmentDateTime",
                table: "Appointments",
                newName: "AppointmentDate");
        }
    }
}
