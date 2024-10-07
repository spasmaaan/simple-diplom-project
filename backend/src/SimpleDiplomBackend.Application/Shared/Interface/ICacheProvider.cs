﻿using Microsoft.Extensions.Caching.Memory;

namespace SimpleDiplomBackend.Application.Shared.Interface
{
    public interface ICacheProvider
    {
        T? GetFromCache<T>(string cacheKey) where T : class;
        void SetCache<T>(string key, T value, DateTimeOffset duration) where T : class;
        void SetCache<T>(string key, T value, MemoryCacheEntryOptions options) where T : class;
        void ClearCache(string key);
    }
}