using System.Collections;
using System.Collections.Generic;

namespace Core.Models
{
    public class Offer
    {
        public int Id { get; set; }
        public string HotelName { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
        public int DurationOfStay { get; set; }
        public decimal Price { get; set; }

        public ICollection<Photo> Photos { get; set; }

        public Country Country { get; set; }
        public int CountryId { get; set; }
        public MealPlan MealPlan { get; set; }
        public int MealPlanId { get; set; }
        public TravelAgency TravelAgency { get; set; }
        public int TravelAgencyId { get; set; }

    }
}