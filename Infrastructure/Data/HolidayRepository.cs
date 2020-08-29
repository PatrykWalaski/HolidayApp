using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class HolidayRepository : GenericRepository<Offer>, IHolidayRepository
    {
        private readonly DataContext _context;
        public HolidayRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Country>> GetCountriesAsync()
        {
            return await _context.Countries.ToListAsync();
        }

        public async Task<Offer> GetHolidayByIdAsync(int id)
        {
            return await _context.Offers.Include(x => x.MealPlan)
                                        .Include(x => x.TravelAgency)
                                        .Include(x => x.Country)
                                        .FirstAsync(x => x.Id == id);
        }

        public async Task<IReadOnlyList<Offer>> GetHolidaysAsync()
        {
            return await _context.Offers.Include(x => x.MealPlan)
                                        .Include(x => x.TravelAgency)
                                        .Include(x => x.Country)
                                        .ToListAsync();
        }

        public async Task<IReadOnlyList<MealPlan>> GetMealPlansAsync()
        {
            return await _context.MealPlans.ToListAsync();
        }

        public async Task<IReadOnlyList<TravelAgency>> GetTravelAgenciesAsync()
        {
            return await _context.TravelAgencies.ToListAsync();
        }
    }
}