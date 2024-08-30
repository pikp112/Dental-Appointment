using AutoMapper;
using DentalAppointment.Core.Models;
using DentalAppointment.Entities.Responses;

namespace DentalAppointment.Core.AutoMapper
{
    public class MappingAppointments : Profile
    {
        public MappingAppointments()
        {
            CreateMap<AppointmentResponse, AppointmentModel>()
                .ReverseMap();
        }
    }
}