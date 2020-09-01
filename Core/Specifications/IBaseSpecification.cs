using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Specifications
{
    public interface IBaseSpecification<T>
    {
         List<Expression<Func<T, bool>>> Filters { get; }
         List<Expression<Func<T, object>>> Includes { get; }
         Expression<Func<T, object>> OrderBy { get; }
         Expression<Func<T, object>> OrderByDescending { get; }
    }
}