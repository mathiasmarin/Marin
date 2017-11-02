using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.DAL.EntityFramework
{
    public interface IDbContext: IDbQueryable
    {
        Task<int> SaveAsync();
        void SaveSync();
        IDbContextTransaction BeginTransaction();

    }
}