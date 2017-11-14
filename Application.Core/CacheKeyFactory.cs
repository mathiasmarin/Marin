using Application.Common;
using Newtonsoft.Json;

namespace Application.Core
{
    public static class CacheKeyFactory
    {
        public static string CreateCacheKey(this ICachedQuery query)
        {
            return JsonConvert.SerializeObject(query);
        }
    }
}
