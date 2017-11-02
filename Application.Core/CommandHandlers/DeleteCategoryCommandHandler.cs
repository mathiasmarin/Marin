using System;
using Application.Common;
using Application.Core.Commands;
using Domain.Common;
using Domain.Core;

namespace Application.Core.CommandHandlers
{
    public class DeleteCategoryCommandHandler:ICommandHandler<DeleteCategoryCommand>
    {
        private readonly IRepository<BudgetCategory> _categoryRepository;

        public DeleteCategoryCommandHandler(IRepository<BudgetCategory> categoryRepository)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        public CommandResult HandleCommand(DeleteCategoryCommand command)
        {
            var cat = _categoryRepository.Get(command.Id);

            _categoryRepository.Remove(cat);

            _categoryRepository.SaveChanges();

            return new CommandResult();


        }
    }
}