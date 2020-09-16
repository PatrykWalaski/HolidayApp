using System;
using System.Linq;
using Core.Models;

namespace Core.Specifications
{
    public class HolidaysWithSpecifications : BaseSpecification<Holiday>
    {
        public HolidaysWithSpecifications(HolidayParams holidayParams, bool isForCount) 
        {
            if(!isForCount)
            {
                AddInclude(x => x.TravelAgency);
                AddInclude(x => x.MealPlan);
                AddInclude(x => x.Country);
                AddInclude(x => x.Photos);
                ApplyPaging(holidayParams.PageSize * (holidayParams.PageIndex - 1), holidayParams.PageSize);
            }
            
            //filtered by names of Countries/Agencies/Meals

            string[] countriesNames  = null;
            string[] agenciesNames = null;
            string[] mealsNames = null ;

            if(holidayParams.Countries != null)
                countriesNames = holidayParams.Countries.Split(',');

            if(holidayParams.Agencies != null)
                agenciesNames = holidayParams.Agencies.Split(',');

            if(holidayParams.Meals != null)
                mealsNames = holidayParams.Meals.Split(',');

            AddFilters(x => (countriesNames == null || countriesNames.Contains(x.Country.Name)) && 
                           (agenciesNames == null || agenciesNames.Contains(x.TravelAgency.Name)) && 
                           (mealsNames == null || mealsNames.Contains(x.MealPlan.Name)));

            // Min and Max Values filter

            if(holidayParams.MinPrice != null)
                AddFilters(x => x.Price >= holidayParams.MinPrice);

            if(holidayParams.MaxPrice != null)
                AddFilters(x => x.Price <= holidayParams.MaxPrice);

            if(holidayParams.MinDuration != null)
                AddFilters(x => x.DurationOfStay >= holidayParams.MinDuration);

            if(holidayParams.MaxDuration != null)
                AddFilters(x => x.DurationOfStay <= holidayParams.MaxDuration);
            
            if(holidayParams.MinStars != null)
                AddFilters(x => x.Stars >= holidayParams.MinStars);


            // Sort Type

        if(!isForCount)
        {
            if(!string.IsNullOrEmpty(holidayParams.Sort))
            {
                switch (holidayParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(x => x.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(x => x.Price);
                        break;
                    case "durationAsc":
                        AddOrderBy(x => x.DurationOfStay);
                        break;
                    case "durationDesc":
                        AddOrderByDescending(x => x.DurationOfStay);
                        break;
                    default: 
                        AddOrderBy(x => x.HotelName);
                        break;
                }
            }
        }
        }        
    }
}