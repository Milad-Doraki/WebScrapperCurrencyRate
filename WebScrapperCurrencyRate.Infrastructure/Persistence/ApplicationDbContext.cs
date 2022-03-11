using WebScrapperCurrencyRate.Application.Common.Interfaces; 
using WebScrapperCurrencyRate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;

namespace WebScrapperCurrencyRate.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
         
        public DbSet<TEntity> DbSet<TEntity>() where TEntity : class
        { 
            return base.Set<TEntity>(); 
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            
            base.OnModelCreating(builder);
        }

        public async Task<TEntity> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : class
        {
            var result = await base.AddAsync(entity, cancellationToken);

            await base.SaveChangesAsync(cancellationToken);

            return (TEntity)result.Entity;
        }

        public bool Any<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return DbSet<TEntity>().Any(predicate);
        }

        public async Task<TEntity> FindAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return await DbSet<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public async Task<IList<TEntity>> FindListAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return await DbSet<TEntity>().Where(predicate).ToListAsync();
        }
    }
}
