# FINBOURNE.CodingExercise

## Task

You have been asked to create a generic in-memory cache component, which other FINBOURNE developers can use in their applications.

- Unique Key
- Store store arbitrary types of objects
- Implement the ‘least recently used’ approach
- Singleton and thread safe
- Notify of eviction

## Usage

### InMemoryCache

```c#

// SET
_cache.Set(key, value, null!);

// GET
var result = _cache.Get(key);

```

## Dependencies

### Compoent

- .NET 6

### Tests

- XUnit
- Fluent Assertions
- TestStack.BDDfy
- Moq
