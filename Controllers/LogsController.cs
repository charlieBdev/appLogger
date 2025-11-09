using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using appLogger.Models;
using Microsoft.EntityFrameworkCore;

namespace appLogger.Controllers
{
    public class LogsController : Controller
    {
        private readonly IConfiguration _config;

        public LogsController(IConfiguration config)
        {
            _config = config;
        }

        public IActionResult Index(string? appName = null, string? envName = null)
        {
            // Load full logging database config from appsettings/user-secrets
            var loggingDatabases = _config.GetSection("LoggingDatabases")
                                        .Get<Dictionary<string, Dictionary<string, string>>>()!;
            if (loggingDatabases == null)
                throw new InvalidOperationException("LoggingDatabases section missing.");

            // --- Safe metadata for client ---
            var envsPerApp = loggingDatabases.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.Keys.ToList()
            );

            ViewBag.Apps = loggingDatabases.Keys.ToList();
            ViewBag.EnvsPerApp = envsPerApp;

            // --- Query logs if app and env are selected ---
            List<Logging> logs = [];
            if (!string.IsNullOrEmpty(appName) && !string.IsNullOrEmpty(envName))
            {
                if (!loggingDatabases.TryGetValue(appName, out var envDict) ||
                    !envDict.TryGetValue(envName, out var connString))
                {
                    return BadRequest("Invalid app or environment.");
                }

                var options = new DbContextOptionsBuilder<LoggingContext>()
                                .UseNpgsql(connString)
                                .Options;

                using var context = new LoggingContext(options);
                logs = [.. context.Loggings.OrderByDescending(l => l.Timestamp)];
            }

            // --- Return JSON for AJAX, or View normally ---
            if (Request.Headers.XRequestedWith == "XMLHttpRequest")
            {
                return Json(logs);
            }

            return View(logs);
        }
    }
}
