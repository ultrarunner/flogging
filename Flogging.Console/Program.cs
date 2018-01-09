using Flogging.Core;
using Serilog;
using System;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Flogging.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            // diagnostic
            var flogDetail = GetFlogDetail("starting application", null);
            Flogger.WriteDiagnostic(flogDetail);

            // perf tracker
            var tracker = new PerfTracker("Flogger.Console Execution", "", flogDetail.UserName, flogDetail.Location, flogDetail.Product, flogDetail.Layer);

            // error
            try
            {
                var ex = new Exception("Something bad has happened");
                ex.Data.Add("input param", "nothing to see here...");
                throw ex;
            }
            catch(Exception ex)
            {
                flogDetail = GetFlogDetail("", ex);
                Flogger.WriteError(flogDetail);
            }

            flogDetail = GetFlogDetail("Flogging.Console Usage", null);
            Flogger.WriteUsage(flogDetail);

            flogDetail = GetFlogDetail("Stopping Flogging.Console", null);
            Flogger.WriteDiagnostic(flogDetail);

            tracker.Stop();
        }

        private static FlogDetail GetFlogDetail(string message, Exception ex)
        {
            return new FlogDetail()
            {
                Product = "Flogger",
                Location = "Flogger.Console",
                Layer = "Job",
                UserName = Environment.UserName,
                HostName = Environment.MachineName,
                Message = message,
                Exception = ex
            };
        }
    }
}
