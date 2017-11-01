using System;
using Application.Common;
using Application.Core.Commands;
using Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marin.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    public class BudgetController : Controller
    {
        private readonly ICommandHandler<AddCategoriesCommand> _addCategoriesCommandHandler;

        public BudgetController(ICommandHandler<AddCategoriesCommand> addCategoriesCommandHandler)
        {
            _addCategoriesCommandHandler = addCategoriesCommandHandler ?? throw new ArgumentNullException(nameof(addCategoriesCommandHandler));
        }
        [HttpPost]
        [Route("Categories")]
        public Guid AddCategories([FromBody]AddCategoriesCommand command)
        {
            command.UserName = User.Identity.Name;
            var test = User.Identity.GetUserId();
            return (Guid) _addCategoriesCommandHandler.HandleCommand(command).Result;
        }
    }
}