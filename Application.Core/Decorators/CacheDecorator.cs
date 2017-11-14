using System;
using Application.Common;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Core.Decorators
{
    public class CacheDecorator<TQuery,TResult>: IQueryHandler<TQuery,TResult> where TQuery:IQuery<TResult> where TResult : class
    {
        private readonly IMemoryCache _cache;
        private readonly IQueryHandler<TQuery, TResult> _decoratedQueryHandler;

        public CacheDecorator(IMemoryCache cache, IQueryHandler<TQuery,TResult> decoratedQueryHandler)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _decoratedQueryHandler = decoratedQueryHandler ?? throw new ArgumentNullException(nameof(decoratedQueryHandler));
        }

        public TResult HandleQuery(TQuery query)
        {
            if (!(query is ICachedQuery cachedQuery))
            {
                return _decoratedQueryHandler.HandleQuery(query);
            }
            var cacheResult = _cache.TryGetValue(cachedQuery.CreateCacheKey(), out TResult value);
            if (cacheResult)
            {
                return value;
            }
            var result = _decoratedQueryHandler.HandleQuery(query);
            _cache.Set(cachedQuery.CreateCacheKey(), result, TimeSpan.FromMinutes(cachedQuery.DurationMinutes));
            return result;
        }
    }
}
