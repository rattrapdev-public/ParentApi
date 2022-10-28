using Microsoft.Extensions.Caching.Memory;
using ToyStoreApi.Models;

namespace ToyStoreApi.Services;

public class ToyCatalogCache : IToyCatalogCache
{
    private readonly IToyCatalogHttpClient _toyCatalogHttpClient;
    private MemoryCache _cache;
    
    public ToyCatalogCache(IToyCatalogHttpClient toyCatalogHttpClient)
    {
        _toyCatalogHttpClient = toyCatalogHttpClient;
        _cache = new MemoryCache(new MemoryCacheOptions());
    }

    public async Task<ToyModel> GetByUpc(string upc)
    {
        if (_cache.TryGetValue(upc, out ToyModel toy))
        {
            return toy;
        }

        var toyToCache = await _toyCatalogHttpClient.GetByUpc(upc);

        _cache.Set(upc, toyToCache);

        return toyToCache;
    }
}