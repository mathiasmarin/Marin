using System;
using Application.Common;
using Application.Core.Dtos;
using Application.Core.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marin.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    public class UserController: Controller
    {
        private readonly IQueryHandler<FindUserQuery, UserDto> _findUserQueryHandler;

        public UserController(IQueryHandler<FindUserQuery,UserDto> findUserQueryHandler)
        {
            _findUserQueryHandler = findUserQueryHandler ?? throw new ArgumentNullException(nameof(findUserQueryHandler));
        }

        [HttpGet]
        [Route("")]
        public UserDto GetUser(FindUserQuery query)
        {
            return _findUserQueryHandler.HandleQuery(new FindUserQuery {UserName = User.Identity.Name});
        }
    }
}
