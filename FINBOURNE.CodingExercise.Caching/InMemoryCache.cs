namespace FINBOURNE.CodingExercise.Caching
{
    using System;
    using System.Collections.Concurrent;

    /// <summary>
    /// Simple memory cache
    /// </summary>
    /// <typeparam name="TKey">Key type</typeparam>
    /// <typeparam name="TValue">Value type</typeparam>
    public class InMemoryCache<TKey, TValue> : ICache<TKey, TValue>
        where TKey : notnull
    {
        private readonly ConcurrentDictionary<TKey, CacheValue<TValue>> _cache;
        private readonly int _maxItems;
        
        public InMemoryCache(int maxItems)
        {
            _cache = new ConcurrentDictionary<TKey, CacheValue<TValue>>();
            _maxItems = maxItems;
        }

        public TValue? Get(TKey key)
        {
            var record = _cache.GetValueOrDefault(key);
            return record != null 
                ? record.Value
                : default(TValue);
        }

        public void Set(TKey key, TValue value, Action<TValue> expiryCallback)
        {
            if (_cache.Count >= _maxItems)
            {
                var oldest = GetLastUsedItem();
                Expire(oldest.Key);
            }

            _cache.TryAdd(key, new CacheValue<TValue>(value, expiryCallback));
        }

        public void Clear()
        {
            foreach (var key in _cache.Keys)
            {
                Expire(key);
            }
        }

        private KeyValuePair<TKey, CacheValue<TValue>> GetLastUsedItem() =>
            _cache.OrderByDescending(item => item.Value.LastUpdated).First(); // todo optimize

        private void Expire(TKey key)
        {
            if (!_cache.Remove(key, out var expired))
            {
                throw new InvalidOperationException("Key Not Found");
            }

            try
            {
                if (expired.ExpiryCallback != null)
                {
                    expired.ExpiryCallback.Invoke(expired.Value);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed Callback", ex);
            }
        }
    }
}