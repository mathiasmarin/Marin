using System;
using System.Collections.Generic;
using Application.Common;
using Application.Core.Commands;
using Application.Core.Dtos;
using Application.Core.Queries;
using Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marin.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    public class BudgetController : Controller
    {
        private readonly ICommandHandler<DeleteCategoryCommand> _deleteCategoryCommandHandler;
        private readonly IQueryHandler<FindMyCategoriesQuery, ICollection<CategoryDto>> _findMyCategoriesQueryHandler;
        private readonly ICommandHandler<AddCategoriesCommand> _addCategoriesCommandHandler;

        public BudgetController(ICommandHandler<AddCategoriesCommand> addCategoriesCommandHandler, 
            IQueryHandler<FindMyCategoriesQuery, ICollection<CategoryDto>> findMyCategoriesQueryHandler,
            ICommandHandler<DeleteCategoryCommand> deleteCategoryCommandHandler)
        {
            _deleteCategoryCommandHandler = deleteCategoryCommandHandler ?? throw new ArgumentNullException(nameof(deleteCategoryCommandHandler));
            _findMyCategoriesQueryHandler = findMyCategoriesQueryHandler ?? throw new ArgumentNullException(nameof(findMyCategoriesQueryHandler));
            _addCategoriesCommandHandler = addCategoriesCommandHandler ?? throw new ArgumentNullException(nameof(addCategoriesCommandHandler));
        }
        [HttpPost]
        [Route("Categories")]
        public Guid AddCategories([FromBody]AddCategoriesCommand command)
        {
            command.UserName = User.Identity.Name;
            return (Guid) _addCategoriesCommandHandler.HandleCommand(command).Result;
        }
        [HttpGet]
        [Route("Categories")]
        public ICollection<CategoryDto> GetCategories()
        {
            return _findMyCategoriesQueryHandler.HandleQuery(new FindMyCategoriesQuery
            {
                UserId = User.Identity.GetUserId()
            });
        }
        [HttpDelete]
        [Route("Category/{Id}")]
        public void DeleteCategory([FromHeader] DeleteCategoryCommand command)
        {
            _deleteCategoryCommandHandler.HandleCommand(command);
        }
    }
}