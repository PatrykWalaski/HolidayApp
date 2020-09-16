using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class HolidayRepository : GenericRepository<Holiday>, IHolidayRepository
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

        public async Task<Holiday> GetHolidayByIdAsync(int id)
        {
            return await _context.Holidays.Include(x => x.MealPlan)
                                        .Include(x => x.TravelAgency)
                                        .Include(x => x.Country)
                                        .Include(x => x.Photos)
                                        .FirstAsync(x => x.Id == id);
        }

        public async Task<IReadOnlyList<Holiday>> GetHolidaysAsync()
        {
            var offers = await _context.Holidays.Include(x => x.MealPlan)
                                        .Include(x => x.TravelAgency)
                                        .Include(x => x.Country)
                                        .Include(x => x.Photos)
                                        .ToListAsync();

            return offers;
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