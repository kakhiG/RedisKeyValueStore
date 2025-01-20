using Microsoft.AspNetCore.Mvc;
using RedisKeyValueStore.Models;

[ApiController]
[Route("api/[controller]")]
public class KeyValueController : ControllerBase
{
    private readonly IRedisService _redisService;

    public KeyValueController(IRedisService redisService)
    {
        _redisService = redisService;
    }

    [HttpPut("{key}")]
    public async Task<IActionResult> SetValue(string key, [FromBody] SetValueRequest request)
    {
        TimeSpan? expiry = request.ExpirySeconds.HasValue
            ? TimeSpan.FromSeconds(request.ExpirySeconds.Value)
            : null;

        var result = await _redisService.SetValueAsync(key, request.Value, expiry);
        return Ok(result);
    }

    [HttpGet("{key}")]
    public async Task<IActionResult> GetValue(string key)
    {
        var value = await _redisService.GetValueAsync(key);
        if (value == null)
            return NotFound();
        return Ok(value);
    }

    [HttpDelete("{key}")]
    public async Task<IActionResult> DeleteValue(string key)
    {
        var result = await _redisService.DeleteValueAsync(key);
        return Ok(result);
    }

    [HttpPost("bulk-get")]
    public async Task<IActionResult> GetMultipleValues([FromBody] string[] keys)
    {
        var results = await _redisService.GetMultipleValuesAsync(keys);
        return Ok(results);
    }

    [HttpPut("{key}/atomic")]
    public async Task<IActionResult> AtomicUpdate(string key, [FromBody] string newValue)
    {
        try
        {
            var result = await _redisService.AtomicUpdateAsync(
                key,
                currentValue => newValue);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Update failed: {ex.Message}");
        }
    }
}
