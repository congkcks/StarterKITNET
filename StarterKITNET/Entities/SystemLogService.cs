using StarterKITNET.Domain;
using StarterKITNET.Entities;
using LogLevel = StarterKITNET.Entities.LogLevel;

public interface ISystemLogService
{
    Task Info(string message, string? source = null, Guid? productId = null);
    Task Warning(string message, string? source = null, Guid? productId = null);
    Task Error(string message, string? source = null, Guid? productId = null);
}

public class SystemLogService : ISystemLogService
{
    private readonly AppDbContext _db;
    public SystemLogService(AppDbContext db) => _db = db;

    public Task Info(string message, string? source = null, Guid? productId = null)
        => Write(LogLevel.Info, message, source, productId);

    public Task Warning(string message, string? source = null, Guid? productId = null)
        => Write(LogLevel.Warning, message, source, productId);

    public Task Error(string message, string? source = null, Guid? productId = null)
        => Write(LogLevel.Error, message, source, productId);

    private async Task Write(LogLevel level, string message, string? source, Guid? productId)
    {
        _db.SystemLogs.Add(new SystemLog
        {
            Id = Guid.NewGuid(),
            Time = DateTimeOffset.UtcNow,
            Level = level,
            Message = message,
            Source = source,
            ProductId = productId
        });
        await _db.SaveChangesAsync();
    }
}