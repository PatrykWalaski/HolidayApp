using System.Linq;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<TEntity> where TEntity : class
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, IBaseSpecification<TEntity> spec)
        {
            var query = inputQuery;

            // if(spec.Filter != null)
            // {
            //     query = query.Where(spec.Filter);
            // }

            // !!!!!!!! FILTERS QUERIES MUST BE BEFORE PAGING !!!!!!!!!!!!
            query = spec.Filters.Aggregate(query, (current, filter) => current.Where(filter));

            if(spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }

            if(spec.OrderByDescending != null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }

            if(spec.IsPagingEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            return query;
        }
    }
}