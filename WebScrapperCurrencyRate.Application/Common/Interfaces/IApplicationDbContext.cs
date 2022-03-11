using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace WebScrapperCurrencyRate.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {

        Task<TEntity> AddAsync<TEntity>([NotNullAttribute] TEntity entity, CancellationToken cancellationToken = default) where TEntity : class;

        bool Any<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;

        Task<TEntity> FindAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
    }
}
