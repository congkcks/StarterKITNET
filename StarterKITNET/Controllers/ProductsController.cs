using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarterKITNET.Domain;
using StarterKITNET.DTOs;
using StarterKITNET.Entities;
namespace MyStarter.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly ISystemLogService _log;
    public ProductsController(AppDbContext db, ISystemLogService log)
    {
        _db = db;
        _log = log;
    }
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var query = _db.Products.AsQueryable();
        var products = await query
            .OrderBy(x => x.Code)
            .Select(x => new
            {
                x.Id,
                x.Code,
                x.Name,
                x.Stock,
                Status = x.Stock == 0 ? "Hết hàng" : "Bình thường"
            })
            .ToListAsync();

        return Ok(products);
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Code = dto.Code,
            Name = dto.Name,
            Stock = dto.Stock!= null ? dto.Stock.Value : 100
        };

        _db.Products.Add(product);
        await _db.SaveChangesAsync();

        await _log.Info(
            $"Tạo sản phẩm {product.Code} - {product.Name}",
            nameof(ProductsController),
            product.Id
        );

        return Ok(product);
    }

    [HttpPost("{id}/import")]
    public async Task<IActionResult> Import(Guid id, int quantity, string? note)
    {
        if (quantity <= 0)
            return BadRequest("Số lượng phải > 0");

        var product = await _db.Products.FindAsync(id);
        if (product == null) return NotFound();

        product.Stock += quantity;

        _db.InventoryTransactions.Add(new InventoryTransaction
        {
            Id = Guid.NewGuid(),
            ProductID = id,
            Quantity = quantity,
            Type = TransactionType.In,
            Time = DateTime.UtcNow,
            Note = note
        });

        await _db.SaveChangesAsync();
        await _log.Info($"Nhập kho {quantity} {product.Name}", nameof(ProductsController), product.Id);
        return Ok("Đã Nhập Hàng Thành Công");
    }
    [HttpPost("{id}/export")]
    public async Task<IActionResult> Export(Guid id, int quantity, string? note)
    {
        if (quantity <= 0)
            return BadRequest("Số lượng phải > 0");

        var product = await _db.Products.FindAsync(id);
        if (product == null) return NotFound();

        if (product.Stock < quantity)
            return BadRequest("Không đủ hàng trong kho");

        product.Stock -= quantity;

        _db.InventoryTransactions.Add(new InventoryTransaction
        {
            Id = Guid.NewGuid(),
            ProductID = id,
            Quantity = quantity,
            Type = TransactionType.Out,
            Time = DateTime.UtcNow,
            Note = note
        });

        await _db.SaveChangesAsync();
        await _log.Info($"Xuất kho {quantity} {product.Name}", nameof(ProductsController), product.Id);

        if (product.Stock == 0)
            await _log.Error($"Sản phẩm \"{product.Name}\" đã hết hàng", nameof(ProductsController), product.Id);
        else if (product.Stock <= 3)
            await _log.Warning($"Sản phẩm \"{product.Name}\" sắp hết hàng (còn {product.Stock})", nameof(ProductsController), product.Id);
        return Ok("Đã Xuất Hàng Thành Công");
    }
    [HttpGet("transactions")]
    public async Task<IActionResult> GetTransactions()
    {
        var transactions = await _db.InventoryTransactions
            .OrderByDescending(x => x.Time)
            .ToListAsync();
        return Ok(transactions);
    }
}