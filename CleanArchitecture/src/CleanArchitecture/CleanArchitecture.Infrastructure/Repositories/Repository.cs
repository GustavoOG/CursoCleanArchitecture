using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Infrastructure.Extensions;
using CleanArchitecture.Infrastructure.Specifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace CleanArchitecture.Infrastructure.Repositories
{
    internal abstract class Repository<TEntity, TEntityId>
        where TEntity : Entity<TEntityId>
        where TEntityId : class
    {
        protected readonly ApplicationDbContext DbContext;

        protected Repository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }
        public async Task<TEntity?> GetByIdAsync(TEntityId Id, CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<TEntity>()
                .FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
        }

        public virtual void Add(TEntity entity)
        {
            DbContext.Add(entity);
        }

        public IQueryable<TEntity> ApplySpecification(ISpecification<TEntity, TEntityId> spec)
        {
            return SpecificationEvaluator<TEntity, TEntityId>.GetQuery(DbContext.Set<TEntity>(), spec);
        }

        public async Task<IReadOnlyList<TEntity>> GetAllWithSpec(ISpecification<TEntity, TEntityId> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<int> CountAsync(ISpecification<TEntity, TEntityId> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        //GetPaginationAsync
        //PageResults
        public async Task<PagedResults<TEntity, TEntityId>> GetPaginationAsync(
            Expression<Func<TEntity, bool>>? predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes,
            int page,
            int pageSize,
            string orderBy,
            bool ascending,
            bool disableTracking = true
            )
        {

            IQueryable<TEntity> query = DbContext.Set<TEntity>();
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }
            if (predicate is not null)
            {
                query = query.Where(predicate);
            }

            if (includes is not null)
            {
                query = includes(query);
            }
            var skipamount = pageSize * (page - 1);
            var totalNumberOfRecords = await query.CountAsync();
            var records = new List<TEntity>();
            if (string.IsNullOrEmpty(orderBy))
            {
                records = await query.Skip(skipamount).Take(pageSize).ToListAsync();
            }
            else
            {
                records = await query.OrderByPropertyOrField(orderBy, ascending)
                    .Skip(skipamount).Take(pageSize).ToListAsync();
            }

            var mod = totalNumberOfRecords % pageSize;
            var totalPageCount = (totalNumberOfRecords / pageSize) + (mod == 0 ? 0 : 1);

            var pageResults = new PagedResults<TEntity, TEntityId>
            {
                Results = records,
                PageNumber = page,
                PageSize = pageSize,
                TotalNumberOfRecords = totalNumberOfRecords,
                TotalNumberOfPages = totalPageCount,
            };

            return pageResults;
        }
    }
}
