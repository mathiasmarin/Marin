using System;
using System.Linq;
using Application.Common;
using Application.Core.Dtos;
using Application.Core.Queries;
using Domain.Core;
using Infrastructure.DAL.EntityFramework;

namespace Infrastructure.DAL.QueryHandlers
{
    public class FindUserQueryHandler: IQueryHandler<FindUserQuery,UserDto>
    {
        private readonly IDbContext _dbContext;

        public FindUserQueryHandler(IDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public UserDto HandleQuery(FindUserQuery query)
        {
            var user = _dbContext.GetSet<User>().FirstOrDefault(x => x.Email.Equals(query.UserName));

            if (user != null)
            {
                return new UserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };
            }
            return new UserDto();
        }
    }
}
