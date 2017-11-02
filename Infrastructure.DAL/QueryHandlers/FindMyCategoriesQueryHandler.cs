using System;
using System.Collections.Generic;
using System.Linq;
using Application.Common;
using Application.Core.Dtos;
using Application.Core.Queries;
using Domain.Core;
using Infrastructure.DAL.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DAL.QueryHandlers
{
    public class FindMyCategoriesQueryHandler:IQueryHandler<FindMyCategoriesQuery, ICollection<CategoryDto>>
    {
        private readonly IDbQueryable _dbContext;

        public FindMyCategoriesQueryHandler(IDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        public ICollection<CategoryDto> HandleQuery(FindMyCategoriesQuery query)
        {
            var user = _dbContext.GetSet<User>().Include(x => x.BudgetCategories)
                .FirstOrDefault(h => h.Id.Equals(query.UserId));

            return user.BudgetCategories.Select(x => new CategoryDto
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }
    }
}