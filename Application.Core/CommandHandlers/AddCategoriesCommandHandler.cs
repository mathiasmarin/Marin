using System;
using System.Collections.Generic;
using System.Linq;
using Application.Common;
using Application.Core.Commands;
using Domain.Common;
using Domain.Core;

namespace Application.Core.CommandHandlers
{
    public class AddCategoriesCommandHandler: ICommandHandler<AddCategoriesCommand>
    {
        private readonly IRepository<User> _userRepository;

        public AddCategoriesCommandHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public CommandResult HandleCommand(AddCategoriesCommand command)
        {
            var user = _userRepository.GetFiltered(x => x.Email.Equals(command.UserName), h => h.BudgetCategories, p => p.Budgets).FirstOrDefault();
            var result = new List<BudgetCategory>();
            foreach (var category in command.Categories)
            {
                var cat = new BudgetCategory(category);
                result.Add(cat);
            }
            user.AddCategories(result);

            _userRepository.Modify(user);

            _userRepository.SaveChanges();


            return new CommandResult(user.Id);


        }
    }
}
