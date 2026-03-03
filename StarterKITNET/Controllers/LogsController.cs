using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarterKITNET.Domain;
namespace StarterKITNET.Controllers;
[ApiController]
[Route("api/[controller]")]
public class LogsController : ControllerBase
{
    private readonly AppDbContext _db;
    public LogsController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<IActionResult> Get(int take = 50, DateTime? date = null)
    {
        var utcToday = (date ?? DateTime.UtcNow).Date;

        var start = new DateTimeOffset(utcToday, TimeSpan.Zero);   // 00:00:00 UTC
        var end = start.AddDays(1);                                 // 00:00 ngày hôm sau

        var logs = await _db.SystemLogs
            .Where(x => x.Time >= start && x.Time < end)
            .OrderByDescending(x => x.Time)
            .Take(take)
            .Select(x => new
            {
                x.Id,
                x.Time,
                Level = x.Level.ToString().ToUpper(),
                x.Message
            })
            .ToListAsync();

        return Ok(logs);
    }
}
