using System.Collections.Generic;
using DatingApp.API.Dtos;

namespace API.Dtos
{
    public class HolidayToReturnDto
    {
        public int Id { get; set; }
        public string HotelName { get; set; }
        public int Stars { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
        public int DurationOfStay { get; set; }
        public decimal Price { get; set; }
        public string Country { get; set; }
        public string MealPlan { get; set; }
        public string TravelAgency { get; set; }
        public ICollection<PhotosForDetailedDto> Photos { get; set; }

    }
}