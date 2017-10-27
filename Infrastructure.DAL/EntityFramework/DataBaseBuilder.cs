using Infrastructure.DAL.EntityFramework.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DAL.EntityFramework
{
    public static class DataBaseBuilder
    {
        public static ModelBuilder BuildDb(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new BudgetCategoryMap());
            modelBuilder.ApplyConfiguration(new MonthlyBudgetMap());
            modelBuilder.ApplyConfiguration(new CostMap());
            return modelBuilder;

        }
    }
}