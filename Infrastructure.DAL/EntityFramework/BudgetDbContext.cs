using System.Threading.Tasks;
using Domain.Common;
using Infrastructure.Security;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.DAL.EntityFramework
{
    public class BudgetDbContext : IdentityDbContext<MarinAppUser>, IDbContext
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
    }
}