using StackExchange.Redis;

public class RedisService : IRedisService
{
    private readonly IConnectionMultiplexer _redis;
    private readonly IDatabase _db;

    public RedisService(IConnectionMultiplexer redis)
    {
        _redis = redis;
        _db = redis.GetDatabase();
    }

    public async Task<bool> SetValueAsync(string key, string value, TimeSpan? expiry = null)
    {
        return await _db.StringSetAsync(key, value, expiry);
    }

    public async Task<string?> GetValueAsync(string key)
    {
        var value = await _db.StringGetAsync(key);
        return value.IsNull ? null : value.ToString();
    }

    public async Task<bool> DeleteValueAsync(string key)
    {
        return await _db.KeyDeleteAsync(key);
    }

    public async Task<IList<string?>> GetMultipleValuesAsync(IEnumerable<string> keys)
    {
        var redisKeys = keys.Select(k => (RedisKey)k).ToArray();
        var values = await _db.StringGetAsync(redisKeys);

        return values.Select(v => v.IsNull ? null : v.ToString()).ToList();
    }

    public async Task<string> AtomicUpdateAsync(string key, Func<string?, string> updateFunction)
    {
        var currentValue = await GetValueAsync(key);
        var newValue = updateFunction(currentValue);
        await _db.StringSetAsync(key, newValue);
        return newValue;
    }
}
