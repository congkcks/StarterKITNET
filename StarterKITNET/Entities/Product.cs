namespace StarterKITNET.Entities;

public class Product
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public int Stock { get; set; }
    public List<InventoryTransaction> InventoryTransactions { get; set; } = new();

}

public enum LogLevel
{
    Info = 1,
    Warning = 2,
    Error = 3
}

public class SystemLog
{
    public Guid Id { get; set; }
    public DateTimeOffset Time { get; set; } = DateTimeOffset.UtcNow;
    public LogLevel Level { get; set; }
    public string Message { get; set; } = "";
    public string? Source { get; set; }
    public Guid? ProductId { get; set; }  
}
