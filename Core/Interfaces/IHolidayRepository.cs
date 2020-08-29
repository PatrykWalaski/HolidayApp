using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Interfaces
{
    public interface IHolidayRepository : IGenericRepository<Offer>
    {
         Task<Offer> GetHolidayByIdAsync(int id);
         Task<IReadOnlyList<Offer>> GetHolidaysAsync();
         Task<IReadOnlyList<MealPlan>> GetMealPlansAsync();
         Task<IReadOnlyList<TravelAgency>> GetTravelAgenciesAsync();
         Task<IReadOnlyList<Country>> GetCountriesAsync();

    }
}