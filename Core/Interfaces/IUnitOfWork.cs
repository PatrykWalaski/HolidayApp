using System;
using System.Threading.Tasks;

namespace Core.Interfaces
{

     public interface IUnitOfWork : IDisposable
    {
         IHolidayRepository Holiday { get; }
         Task<int> Complete();
    }
}