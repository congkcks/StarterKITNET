namespace StarterKITNET.Entities
{
    public class InventoryTransaction
    {
        public Guid Id { get; set; }
        public DateTimeOffset Time { get; set; }=DateTimeOffset.UtcNow;
        public TransactionType Type { get; set; }
        public Guid ProductID { get; set; }
        public int Quantity { get; set; }
        public string? Note { get; set; }
        public Product Product { get; set; } = default;
    }
}
