namespace RedisKeyValueStore.Models
{
    public class UpdateValueRequest
    {
        public string Operation { get; set; } = string.Empty;
        public object? Parameter { get; set; }

        public object UpdateFunction(object? currentValue)
        {

            switch (Operation.ToLower())
            {
                case "increment":
                    return Convert.ToInt32(currentValue ?? 0) + Convert.ToInt32(Parameter);
                case "append":
                    return (currentValue?.ToString() ?? "") + Parameter?.ToString();
                default:
                    throw new ArgumentException("Unsupported operation");
            }
        }
    }
}
