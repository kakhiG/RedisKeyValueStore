namespace RedisKeyValueStore.Data
{
    public class DataSeeder
    {
        private readonly IRedisService _redisService;

        public DataSeeder(IRedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task SeedAsync()
        {
            try
            {
                await _redisService.SetValueAsync("user1", "David Jay");
                await _redisService.SetValueAsync("user2", "George Patrick");
                await _redisService.SetValueAsync("user3", "Liza Johnson");

                await _redisService.SetValueAsync("product1", "Software");
                await _redisService.SetValueAsync("product2", "Smartphone");
                await _redisService.SetValueAsync("product3", "Headphones");

                await _redisService.SetValueAsync("temp1", "Temporary Value 1", TimeSpan.FromMinutes(5));
                await _redisService.SetValueAsync("temp2", "Temporary Value 2", TimeSpan.FromHours(1));

                await _redisService.SetValueAsync("counter:visits", "100");

                Console.WriteLine("Successfully seeded data!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error seeding data: {ex.Message}");
                throw;
            }
        }
    }
}