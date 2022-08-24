namespace FINBOURNE.CodingExercise.Caching
{
    /// <summary>
    /// Generic Caching Store
    /// </summary>
    public interface ICache<TKey, TValue>
    {
        /// <summary>
        /// Store a generic value with defined key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiryCallback">Callback when an item is expired</param>
        void Set(TKey key, TValue value, Action<TValue> expiryCallback);

        /// <summary>
        /// Returns value at key
        /// </summary>
        /// <param name="key"></param>
        /// <returns><see cref="default(TValue)"/> if nothing was found with matching</returns>
        TValue? Get(TKey key);

        /// <summary>
        /// Remove all values stored
        /// </summary>
        void Clear();
    }
}
