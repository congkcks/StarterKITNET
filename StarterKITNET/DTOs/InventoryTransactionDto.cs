namespace StarterKITNET.DTOs;
public record InventoryTransactionDto(
    int Id,
    int ProductId,
    string ProductName,
    decimal Qty,
    DateTime CreatedAt
);
