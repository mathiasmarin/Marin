using System;
using System.Threading.Tasks;

namespace Domain.Common
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        void Add(TEntity entity);
        TEntity Get(Guid id);
        void Remove(TEntity entity);
        void Modify(TEntity entity);
        Task<bool> SaveChangesAsync();
        void SaveChanges();

    }
}