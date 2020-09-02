using API.Dtos;
using AutoMapper;
using Core.Models;

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

        }
    }
}