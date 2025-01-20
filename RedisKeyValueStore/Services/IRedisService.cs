public interface IRedisService
{
    Task<bool> SetValueAsync(string key, string value, TimeSpan? expiry = null);
    Task<string?> GetValueAsync(string key);
    Task<bool> DeleteValueAsync(string key);
    Task<IList<string?>> GetMultipleValuesAsync(IEnumerable<string> keys);
    Task<string> AtomicUpdateAsync(string key, Func<string?, string> updateFunction);
}