public class CustomerSummary
{
    public int Id { get; set; }
    public string Name { get; set; } = "";

    public override bool Equals(object? obj) { /* ... */ }
    public override int GetHashCode() { /* ... */ }
}
