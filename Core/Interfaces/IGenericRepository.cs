using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Specifications;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
         Task<T> GetByIdAsync(int id);
         Task<IReadOnlyList<T>> ListAllAsync();
         Task<IReadOnlyList<T>> ListWithSpecAsync(IBaseSpecification<T> spec);
         Task<int> CountAsync(IBaseSpecification<T> spec);
         void Add(T entity);
         void Update(T entity);
         void Delete(T entity);
         void Delete(int id);
    }
}