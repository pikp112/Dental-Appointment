using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DentalAppointment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingIsReject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRejected",
                table: "Appointments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRejected",
                table: "Appointments");
        }
    }
}
