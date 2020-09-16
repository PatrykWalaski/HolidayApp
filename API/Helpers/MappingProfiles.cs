using API.Dtos;
using AutoMapper;
using Core.Models;
using DatingApp.API.Dtos;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Holiday, HolidayToReturnDto>()
            .ForMember(d => d.Country, o => o.MapFrom(s => s.Country.Name))
            .ForMember(d => d.MealPlan, o => o.MapFrom(s => s.MealPlan.Name))
            .ForMember(d => d.TravelAgency, o => o.MapFrom(s => s.TravelAgency.Name));

            CreateMap<Photo, PhotosForDetailedDto>();
            CreateMap<PhotoForCreationDto, Photo>();
            CreateMap<Photo, PhotoForReturnDto>();

        }
    }
}