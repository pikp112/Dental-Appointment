using AutoMapper;
using DentalAppointment.Core.Dtos;
using DentalAppointment.Core.Models;
using DentalAppointment.Entities.Responses;

namespace DentalAppointment.Core.AutoMapper
{
    public class MappingAppointments : Profile
    {
        public MappingAppointments()
        {
            CreateMap<AppointmentDto, AppointmentModel>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Duration, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<AppointmentResponse, AppointmentModel>()
                .ReverseMap();
        }
    }
}