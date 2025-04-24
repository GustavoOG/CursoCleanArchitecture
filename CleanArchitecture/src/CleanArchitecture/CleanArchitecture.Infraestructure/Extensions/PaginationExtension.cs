using System.Linq.Expressions;

namespace CleanArchitecture.Infraestructure.Extensions
{
    public static class PaginationExtension
    {
        public static IQueryable<T> OrderByPropertyOrField<T>(
            this IQueryable<T> queryable,
            string propertyOrFieldName,
            bool ascending = true)
        {
            var elementType = typeof(T);
            var orderByMethod = ascending ? "OrderBy" : "OrderByDescending";

            var parameterExpression = Expression.Parameter(elementType);
            var propertyOrFieldExpression = Expression.PropertyOrField(parameterExpression, propertyOrFieldName);

            var selector = Expression.Lambda(propertyOrFieldExpression, parameterExpression);

            var orderByExpression = Expression.Call(
                typeof(Queryable),
                orderByMethod,
                new Type[] { elementType, propertyOrFieldExpression.Type },
                queryable.Expression,
                Expression.Quote(selector));

            return queryable.Provider.CreateQuery<T>(orderByExpression);
        }
    }
}
