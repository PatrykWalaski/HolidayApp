using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.Models;

namespace Core.Specifications
{
    public class BaseSpecification<T> : IBaseSpecification<T>
    {   
        public List<Expression<Func<T, bool>>> Filters { get; } = new List<Expression<Func<T, bool>>>();

        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderBy {get; private set;}

        public Expression<Func<T, object>> OrderByDescending {get; private set;}

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        protected void AddFilters(Expression<Func<T, bool>> whereExpression)
        {
            Filters.Add(whereExpression);
        }

        protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
        {
            OrderByDescending = orderByDescExpression;
        }

        // paging 

        public int Take {get; private set;}
        public int Skip {get; private set;}
        public bool IsPagingEnabled {get; private set;}

        protected void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }
    }
}