using System;
using Application.Common;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Core.Decorators
{
    public class CacheRemover<TCommand>:ICommandHandler<TCommand> where TCommand : ICommand
    {
        private readonly IMemoryCache _cache;
        private readonly ICommandHandler<TCommand> _decoratedCommandHandler;

        public CacheRemover(IMemoryCache cache, ICommandHandler<TCommand> decoratedCommandHandler)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _decoratedCommandHandler = decoratedCommandHandler ?? throw new ArgumentNullException(nameof(decoratedCommandHandler));
        }

        public void Execute(TCommand command)
        {
            if (!(command is ICacheRemoverCommand cacheCommand))
            {
                _decoratedCommandHandler.Execute(command);
                
            }
            else
            {
                
                _decoratedCommandHandler.Execute(command);
                var uniqueProp = cacheCommand.GetType().GetProperty(cacheCommand.Query.NameOfUniqueProperty);
                var value = uniqueProp.GetValue(command);
                cacheCommand.Query.GetType().GetProperty(cacheCommand.Query.NameOfUniqueProperty).SetValue(cacheCommand.Query,value);
                _cache.Remove(cacheCommand.Query.CreateCacheKey()); 
                
            }
        }
    }
}
