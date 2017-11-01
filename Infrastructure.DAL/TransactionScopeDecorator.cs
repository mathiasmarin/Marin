using System;
using Application.Common;
using Infrastructure.DAL.EntityFramework;

namespace Infrastructure.DAL
{
    public class TransactionScopeDecorator<TCommand>:ICommandHandler<TCommand> where TCommand:ICommand
    {
        private readonly IDbContext _dbContext;
        private readonly ICommandHandler<TCommand> _decoratedHandler;

        public TransactionScopeDecorator(ICommandHandler<TCommand> decoratedHandler, IDbContext dbContext)
        {
            _dbContext = dbContext;
            _decoratedHandler = decoratedHandler ?? throw new ArgumentNullException(nameof(decoratedHandler));
        }

        public CommandResult HandleCommand(TCommand command)
        {
            // So EF core does not support ambient transactionsscope. To controll transactions i need to create one in the dbcontext like this. 
            // Do not like that our idbcontext has a begintransaction method. Maybe create another interface just for that?
            // https://docs.microsoft.com/en-us/ef/core/saving/transactions
            // https://stackoverflow.com/questions/45919011/how-to-implement-ambient-transaction-in-entity-framework-core

            using (var transaction = _dbContext.BeginTransaction())
            {
                var result = _decoratedHandler.HandleCommand(command);

                transaction.Commit();

                return result;
            }
        }
    }
}