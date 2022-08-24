namespace FINBOURNE.CodingExercise.Caching.Tests
{
    using FluentAssertions;

    public class InMemoryCacheTests
    {
        private readonly InMemoryCache<string, string> _cache;

        public InMemoryCacheTests()
        {
            _cache = new InMemoryCache<string, string>(1);
        }

        [Fact]
        public void AssignedInterface()
        {
            _cache.Should().BeAssignableTo<ICache<string, string>>();
        }

        [Fact]
        public void CanGetAndSet()
        {
            // Given
            var key = "key";
            var value = "value";

            // When
            _cache.Set(key, value, null!);
            var result = _cache.Get(key);

            // Then
            result.Should().Be(value);
        }

        [Fact]
        public void CanGetAndSet_Null()
        {
            // Given 
            var key = "key";
            string value = null!;

            // When
            _cache.Set(key, value, null!);
            var result = _cache.Get(key);

            // Then
            result.Should().Be(null!);
        }

        [Fact]
        public void CanUpdateExistingValue()
        {
            // Given 
            var key = "key";
            string value1 = "value1";
            string value2 = "value2";

            // When
            _cache.Set(key, value1, null!);
            _cache.Set(key, value2, null!);

            var result = _cache.Get(key);

            // Then
            result.Should().Be(value2);
        }

        [Fact]
        public void AboveThresholdExpiresOldestItem()
        {
            // Given
            string actualExpiredValue = string.Empty;
            _cache.Set("key", "value", (expired) =>
            {
                actualExpiredValue = expired;
            });

            // When
            _cache.Set("2ndKey", "2ndValue", null!);

            // Then
            actualExpiredValue.Should().Be("value");
            _cache.Get("key").Should().Be(null);
            _cache.Get("2ndKey").Should().Be("2ndValue");
        }

        [Fact]
        public void AboveThresholdExpiresOldestItemWithNoCallback()
        {
            // Given
            _cache.Set("key", "value", null!);

            // When
            _cache.Set("2ndKey", "2ndValue", null!);

            // Then            
            _cache.Get("2ndKey").Should().Be("2ndValue");
        }

        [Fact]
        public void ClearInvokesCallback()
        {
            // Given
            var isCallBackInvoked = false;
            _cache.Set("key", "value", (expired) => isCallBackInvoked = true);

            // When
            _cache.Clear();

            // Then
            isCallBackInvoked.Should().BeTrue();
        }
    }
}
