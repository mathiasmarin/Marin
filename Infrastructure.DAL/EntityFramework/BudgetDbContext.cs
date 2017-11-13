using System.Threading.Tasks;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.DAL.EntityFramework
{
    public class BudgetDbContext :DbContext, IDbContext
    {
        private readonly IConfiguration _config;

        public BudgetDbContext(IConfiguration config) : base(new DbContextOptions<BudgetDbContext>())
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_config["ConnectionStrings:MyConnectionString"]);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Customizations must go after base.OnModelCreating(builder) Ref: https://scottsauber.com/2017/09/11/customizing-ef-core-2-0-with-ientitytypeconfiguration/

            DataBaseBuilder.BuildDb(builder);
            
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

        public IDbContextTransaction BeginTransaction()
        {
            return Database.BeginTransaction();
        }
    }
}