using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using WebScrapperCurrencyRate.Application.Common.Interfaces;

namespace WebScrapperCurrencyRate.Infrastructure.Persistence
{
    public class MongoDbContext : IApplicationDbContext
    {
        private IMongoDatabase _database { get; set; }
        private MongoClient _mongoClient { get; set; }

        protected MongoDbContext()
        {

        }

        public MongoDbContext(IOptions<MongoDbSettings> configuration)
        {
            _mongoClient = new MongoClient(configuration.Value.ConnectionString);

            _database = _mongoClient.GetDatabase(configuration.Value.DatabaseName);
        }

        public IMongoCollection<TEntity> Collection<TEntity>() where TEntity : class
        {
            return _database.GetCollection<TEntity>(typeof(TEntity).Name);
        }


        public async Task<TEntity> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken) where TEntity : class
        {
            Collection<TEntity>().InsertOne(entity);

            return entity;
        }

        public bool Any<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return Collection<TEntity>().CountDocuments(predicate) > 0;
        }

        public async Task<TEntity> FindAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return await Collection<TEntity>().Find(predicate).FirstOrDefaultAsync();
        }
    }
}
