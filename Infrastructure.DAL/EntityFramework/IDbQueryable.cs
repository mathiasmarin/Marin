using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DAL.EntityFramework
{
    /// <summary>
    /// When querying the db, use this!
    /// </summary>
    public interface IDbQueryable
    {
        DbSet<TEntity> GetSet<TEntity>() where TEntity : Entity;

    }
}