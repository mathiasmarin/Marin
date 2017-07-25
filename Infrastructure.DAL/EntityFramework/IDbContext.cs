using System.Threading.Tasks;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DAL.EntityFramework
{
    public interface IDbContext
    {
        DbSet<TEntity> GetSet<TEntity>() where TEntity : Entity;
        Task<int> SaveAsync();
        void SaveSync();
    }
}