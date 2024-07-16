using Microsoft.EntityFrameworkCore;
using OrderSystem.Core.Specofication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Repository
{
    public static class SpecificationEvalutor<T> where T : class
    {
        public static async Task<IQueryable<T>> GetQuery(IQueryable<T> DbContext, ISpecifications<T> Spec)
        {
            var Query = DbContext;

            if (Spec.Criteria is not null)
                Query = Query.Where(Spec.Criteria);


            if (Spec.OrderBy is not null)
                Query = Query.OrderBy(Spec.OrderBy);

            else if (Spec.OrderByDesc is not null)
                Query = Query.OrderByDescending(Spec.OrderByDesc);


            if (Spec.Includes is not null)
                Query = Spec.Includes.Aggregate(Query, (currentquery, includeexpression) => currentquery.Include(includeexpression));

            return Query;
        }
    }
}
