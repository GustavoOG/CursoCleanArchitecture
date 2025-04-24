using System.Linq.Expressions;

namespace CleanArchitecture.Domain.Abstractions
{
    public abstract class BaseSpecification<TEntity, TEntityId> : ISpecification<TEntity, TEntityId>
        where TEntity : Entity<TEntityId>
        where TEntityId : class
    {
        public BaseSpecification()
        {

        }

        public BaseSpecification(Expression<Func<TEntity, bool>>? criteria)
        {
            Criteria = criteria;
        }


        public Expression<Func<TEntity, bool>>? Criteria { get; }
        public List<Expression<Func<TEntity, Object>>> Includes { get; } = new List<Expression<Func<TEntity, Object>>>();
        public Expression<Func<TEntity, Object>>? OrderBy { get; private set; }
        public Expression<Func<TEntity, Object>>? OrderByDescending { get; private set; }
        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPagingEnable { get; private set; }

        protected void AddOrderBy(Expression<Func<TEntity, Object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        protected void AddOrderByDescending(Expression<Func<TEntity, Object>> orderByDescendingExpression)
        {
            OrderByDescending = orderByDescendingExpression;
        }

        protected void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnable = true;
        }

        protected void AddInclude(Expression<Func<TEntity, Object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
    }
}
