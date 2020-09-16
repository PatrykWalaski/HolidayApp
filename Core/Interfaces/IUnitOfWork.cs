using System;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Interfaces
{

     public interface IUnitOfWork : IDisposable
    {
         IHolidayRepository Holiday { get; }
         IGenericRepository<Photo> Photo { get; }
         Task<int> Complete();
    }
}