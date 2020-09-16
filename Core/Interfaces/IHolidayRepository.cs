using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Interfaces
{
    public interface IHolidayRepository : IGenericRepository<Holiday>
    {
         Task<Holiday> GetHolidayByIdAsync(int id);
         Task<IReadOnlyList<Holiday>> GetHolidaysAsync();
         Task<IReadOnlyList<MealPlan>> GetMealPlansAsync();
         Task<IReadOnlyList<TravelAgency>> GetTravelAgenciesAsync();
         Task<IReadOnlyList<Country>> GetCountriesAsync();
         


    }
}