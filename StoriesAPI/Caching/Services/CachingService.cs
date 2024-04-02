using Microsoft.Extensions.Caching.Memory;
using StoriesAPI.Caching.Abstraction;
using StoriesAPI.Caching.Models;
using System;
using System.Threading.Tasks;

namespace StoriesAPI.Caching.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class CachingService : ICachingService
    {
        
        private CacheResultModel result;
        private IMemoryCache _cache;

        /// <summary>
        /// Initializes a new instance of the <see cref="CachingService"/> class.
        /// </summary>
        /// <param name="cache">The cache.</param>
        /// <param name="constantProvider">The constant provider.</param>
        public CachingService(IMemoryCache cache)
        {
            _cache = cache;
        }
        #region RETRIEVE

        /// <summary>
        /// Retrieves from cache asynchronous.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public async Task<CacheResultModel> RetrieveFromCacheAsync(string key)
        {
            result = new CacheResultModel(key);
            try
            {
                await Task.Run(() =>
                {
                    if (_cache.Get(key) == null)
                    {
                        result.CacheStatus = CacheResultModel.CacheStatusOption.DoesNotExists;
                    }
                    else
                    {
                        result.CacheStatus = CacheResultModel.CacheStatusOption.Exists;
                        result.CacheValue = _cache.Get(key);
                    }
                });
            }
            catch (Exception error)
            {
                result.CacheStatus = CacheResultModel.CacheStatusOption.Error;
                result.Error = error;
            }
            return result;
        }
        #endregion

        #region STORING

        /// <summary>
        /// Saves to cache asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="objectToCache">The object to cache.</param>
        /// <param name="expirationTimeLimit">The expiration time limit.</param>
        /// <returns></returns>
        public async Task<CacheResultModel> SaveToCacheAsync<T>(string key, T objectToCache, int? expirationTimeLimit = null)
        {
            result = new CacheResultModel(key);
            object cacheObject = null;
            try
            {
                await Task.Run(() =>
                {
                    if (!_cache.TryGetValue(key, out cacheObject))
                    {
                        cacheObject = Newtonsoft.Json.JsonConvert.SerializeObject(objectToCache);
                        var memoryCacheEntryOptions = new MemoryCacheEntryOptions();
                        // memoryCacheEntryOptions.RegisterPostEvictionCallback(CacheExpired_Callback);
                        memoryCacheEntryOptions.SetAbsoluteExpiration(TimeSpan.FromMinutes(expirationTimeLimit ?? 180));
                        //memoryCacheEntryOptions.AbsoluteExpiration = DateTime.Now.AddMinutes(_constantProvider.SmartsConstants.CacheAbsoluteExpirationInMinutes);
                        //memoryCacheEntryOptions.SlidingExpiration = TimeSpan.FromSeconds(60);
                        //memoryCacheEntryOptions.AddExpirationToken(new CancellationChangeToken(new CancellationTokenSource(TimeSpan.FromSeconds(120)).Token));
                        _cache.Set(key, objectToCache, memoryCacheEntryOptions);
                    }
                });
                result.CacheValue = cacheObject;
                result.CacheStatus = CacheResultModel.CacheStatusOption.Cached;
            }
            catch (Exception error)
            {
                result.CacheStatus = CacheResultModel.CacheStatusOption.Error;
                result.Error = error;
            }
            return result;
        }
        #endregion

        #region CacheExpired

        /// <summary>
        /// Caches the expired callback.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="reason">The reason.</param>
        /// <param name="state">The state.</param>
        private async void CacheExpired_Callback(object key, object value, EvictionReason reason, object state)
        {
            var existingDataInCache = await RetrieveFromCacheAsync(key.ToString());

            if (existingDataInCache.CacheStatus == CacheResultModel.CacheStatusOption.DoesNotExists)
            {
                await SaveToCacheAsync(key.ToString(), value);
            }
        }
        #endregion
    }
}
