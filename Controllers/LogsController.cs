using Microsoft.AspNetCore.Mvc;
using appLogger.Models;

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
            var loggingDatabases = _config.GetSection("LoggingDatabases")
                                          .Get<Dictionary<string, Dictionary<string, string>>>()!;

            if (loggingDatabases == null)
                throw new InvalidOperationException("LoggingDatabases section missing in appsettings.json");

            // Pass full dictionary for JS environment refresh
            ViewBag.LoggingDatabases = loggingDatabases;

            // App dropdown list
            ViewBag.Apps = loggingDatabases.Keys.ToList();

            // Environment dropdown list
            ViewBag.Envs = appName != null && loggingDatabases.ContainsKey(appName)
                          ? loggingDatabases[appName].Keys.ToList()
                          : [];

            // Selected values
            ViewBag.SelectedApp = appName ?? "";
            ViewBag.SelectedEnv = envName ?? "";

            // Logs
            List<Logging> logs = new List<Logging>();

            if (!string.IsNullOrEmpty(appName) && !string.IsNullOrEmpty(envName))
            {
                var connString = loggingDatabases[appName][envName];
                using var context = new LoggingContext(connString);
                logs = [.. context.Loggings.OrderByDescending(l => l.Timestamp)];
            }

            return View(logs);
        }
    }
}
