namespace FINBOURNE.CodingExercise.Caching
{
    using System;

    /// <summary>
    /// Cached Value
    /// </summary>
    /// <typeparam name="TValue">User defined base value</typeparam>
    internal class CacheValue<TValue>
    {
        /// <summary>
        /// Timestamp of last updated
        /// </summary>
        public DateTime LastUpdated { get; }

        /// <summary>
        /// Base value 
        /// </summary>
        public TValue Value { get; }

        /// <summary>
        /// Notification on expiry
        /// </summary>
        public Action<TValue> ExpiryCallback { get; } // todo multi-callback with array

        /// <summary>
        /// Primary constructor
        /// </summary>
        /// <param name="value"></param>
        /// <param name="callback"></param>
        public CacheValue(TValue value, Action<TValue> callback)
        {
            LastUpdated = DateTime.UtcNow;
            Value = value;
            ExpiryCallback = callback;
        }

        /// <summary>
        /// Creates a new copy with an updated value
        /// </summary>
        /// <param name="updateValue"></param>
        /// <returns></returns>
        public CacheValue<TValue> Clone(TValue updateValue) =>
            new CacheValue<TValue>(updateValue, ExpiryCallback);
    }
}
