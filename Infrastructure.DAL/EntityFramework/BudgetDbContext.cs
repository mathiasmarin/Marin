
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Threading.Tasks;
using Domain.Common;

namespace Infrastructure.DAL.EntityFramework
{
    public class BudgetDbContext: DbContext, IDbContext
    {
        private readonly IConfigurationRoot _config;

        public BudgetDbContext(IConfigurationRoot config, DbContextOptions options) : base(options)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_config["ConnectionStrings:MyConnectionString"]);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            DataBaseBuilder.BuildDb(modelBuilder);
        }

        public DbSet<TEntity> GetSet<TEntity>() where TEntity : Entity
        {
            return Set<TEntity>();
        }

        public async Task<int> SaveAsync()
        {
            return await SaveChangesAsync();
        }

        public void SaveSync()
        {
            SaveChanges();
        }
    }
}
