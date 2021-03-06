using System.Threading.Tasks;
using Core.Interfaces;
using Core.Models;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public IHolidayRepository Holiday { get; private set; }
        public IGenericRepository<Photo> Photo { get; private set; }

        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
            Holiday = new HolidayRepository(_context);
            Photo = new GenericRepository<Photo>(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }
    }
}