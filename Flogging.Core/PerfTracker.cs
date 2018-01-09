using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace Flogging.Core
{
    public class PerfTracker
    {
        private readonly Stopwatch _stopWatch;
        private readonly FlogDetail _log;

        public PerfTracker(string name, string userId, string userName, string location, string product, string layer)
        {
            _stopWatch = Stopwatch.StartNew();
            _log = new FlogDetail()
            {
                Message = name,
                UserId = userId,
                UserName = userName,
                Product = product,
                Layer = layer,
                Location = location,
                HostName = Environment.MachineName
            };

            var beginTime = DateTime.UtcNow;
            _log.AdditionalInfo = new Dictionary<string, object>()
            {
                {"Started", beginTime.ToString(CultureInfo.InstalledUICulture)}
            };
        }

        public PerfTracker(string name, string userId, string userName, string location, string product, string layer, Dictionary<string, object> perfParams)
            // calling core constructor
            :this(name, userId,  userName, location, product, layer)
        {
            foreach(var param in perfParams)
            {
                _log.AdditionalInfo.Add("input-" + param.Key, param.Value);
            }
        }

        public void Stop()
        {
            _stopWatch.Stop();
            _log.ElapsedMilliseconds = _stopWatch.ElapsedMilliseconds;
            Flogger.WritePerf(_log);
        }
    }
}
