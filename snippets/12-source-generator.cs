[JsonSerializable(typeof(Customer))]
internal partial class AppJsonContext : JsonSerializerContext
{
}

var json = JsonSerializer.Serialize(
    customer,
    AppJsonContext.Default.Customer);
