using Domain.Common;
using Infrastructure.DAL.EntityFramework.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DAL.EntityFramework
{
    public static class DataBaseBuilder
    {
        public static void BuildDb(ModelBuilder modelBuilder)
        {
            new UserMap(modelBuilder.Entity<User>());
        }
    }
}