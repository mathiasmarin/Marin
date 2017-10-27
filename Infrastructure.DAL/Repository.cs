using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common;
using Infrastructure.DAL.EntityFramework;

namespace Infrastructure.DAL
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        private readonly IDbContext _dbContext;

        public Repository(IDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public void Add(TEntity entity)
        {
            _dbContext.GetSet<TEntity>().Add(entity);
        }

        public TEntity Get(Guid id)
        {
            return _dbContext.GetSet<TEntity>().FirstOrDefault(x => x.Id.Equals(id));
        }

        public void Remove(TEntity entity)
        {
            _dbContext.GetSet<TEntity>().Remove(entity);
        }

        public void Modify(TEntity entity)
        {
            _dbContext.GetSet<TEntity>().Update(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveAsync() > 0;
        }

        public void SaveChanges()
        {
            _dbContext.SaveSync();
        }
    }
}