using System.Threading.Tasks;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.DAL.EntityFramework
{
    public interface IDbContext
    {
        DbSet<TEntity> GetSet<TEntity>() where TEntity : Entity;
        Task<int> SaveAsync();
        void SaveSync();
        IDbContextTransaction BeginTransaction();

    }
}