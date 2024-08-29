using DentalAppointment.Core.Enums;
using DentalAppointment.Core.Models;

namespace DentalAppointment.Infrastructure.Repositories.Contracts
{
    public interface IAppointmentRepository : IGenericRepository<AppointmentModel>
    {
        Task<AppointmentModel> CreateAppointmentAsync(Guid appointmentId, DateTime appointmentDateTime, string patientName, string patientPhoneNumber, TreatmentType treatmentType, string notes);

        Task<AppointmentModel> UpdateAppointmentAsync(DateTime actualAppointmentDateTime, DateTime? newAppointmentDateTime, string? patientName, string? patientPhoneNumber, TreatmentType? treatmentType, string? notes, bool? isConfirmed);

        Task<AppointmentModel> DeleteAppointmentAsync(DateTime appointmentDateTime);

        Task<AppointmentModel?> GetByDateAsync(DateTime appointmentDate);

        Task<IReadOnlyCollection<AppointmentModel>> GetAll();
    }
}