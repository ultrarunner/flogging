using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using System;
using System.IO;

namespace Flogging.Core
{
    public static class Flogger
    {
        public static IConfigurationRoot Configuration { get; set; }

        private static readonly ILogger _perfLogger;
        private static readonly ILogger _usageLogger;
        private static readonly ILogger _errorLogger;
        private static readonly ILogger _diagnosticLogger;

        static Flogger()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            Configuration = builder.Build();

            _perfLogger = new LoggerConfiguration()
                .WriteTo.File(path: @"C:\Users\jerom\logs\perf.txt")
                .CreateLogger();

            _usageLogger = new LoggerConfiguration()
                .WriteTo.File(path: @"C:\Users\jerom\logs\usage.txt")
                .CreateLogger();

            _errorLogger = new LoggerConfiguration()
                .WriteTo.File(path: @"C:\Users\jerom\logs\error.txt")
                .CreateLogger();

            _diagnosticLogger = new LoggerConfiguration()
                .WriteTo.File(path: @"C:\Users\jerom\logs\diagnostic.txt")
                .CreateLogger();
        }

        public static void WritePerf(FlogDetail log)
        {
            _perfLogger.Write(LogEventLevel.Information, "{@FlogDetail}", log);
        }

        public static void WriteUsage(FlogDetail log)
        {
            _usageLogger.Write(LogEventLevel.Information, "{@FlogDetail}", log);
        }

        public static void WriteError(FlogDetail log)
        {
            _errorLogger.Write(LogEventLevel.Information, "{@FlogDetail}", log);
        }

        public static void WriteDiagnostic(FlogDetail log)
        {
            bool enableDiagnostics = Convert.ToBoolean(Configuration["EnableDiagnostics"]);
            if (!enableDiagnostics)
                return;
            _diagnosticLogger.Write(LogEventLevel.Information, "{@FlogDetail}", log);
        }

    }
}
