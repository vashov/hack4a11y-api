using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Api.Infrastructure.Extensions
{
    // Source: https://medium.com/@pawel.gerr/entity-framework-core-making-rownumber-more-usefull-c1e177d7d877
    public static class QueryableExtensions
    {
        private static readonly MethodInfo _asQueryableMethodInfo
              = typeof(Queryable)
                 .GetMethods(BindingFlags.Public | BindingFlags.Static)
                 .Single(m => m.Name == nameof(Queryable.AsQueryable)
                                        && m.IsGenericMethod);
        public static IQueryable<TEntity> AsSubQuery<TEntity>(
                              this IQueryable<TEntity> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (!(source.Provider is EntityQueryProvider))
                return source;
            var methodCall = Expression.Call(
                null,
                _asQueryableMethodInfo.MakeGenericMethod(typeof(TEntity)),
                source.Expression);
            return source.Provider.CreateQuery<TEntity>(methodCall);
        }
    }
}
