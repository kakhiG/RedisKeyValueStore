namespace RedisKeyValueStore.Models
{
    public class SetValueRequest
    {
        public string Value { get; set; } = string.Empty;
        public int? ExpirySeconds { get; set; }
    }
}
