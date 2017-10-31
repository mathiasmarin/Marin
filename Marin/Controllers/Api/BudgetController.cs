using System;
using Application.Common;
using Application.Core.Commands;
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
        public void AddCategories([FromBody]AddCategoriesCommand command)
        {
            command.UserName = User.Identity.Name;
            _addCategoriesCommandHandler.HandleCommand(command);
        }
    }
}