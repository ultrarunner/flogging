using System;
using System.Collections.Generic;
using System.Text;

namespace Flogging.Core
{
    public class FlogDetail
    {
        public FlogDetail()
        {
            Timestamp = DateTime.UtcNow;
        }

        public DateTime Timestamp { get; private set; }
        public string Message { get; set; }

        // where
        public string Product { get; set; }
        public string Layer { get; set; }
        public string Location { get; set; }
        public string HostName { get; set; }

        // who
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }

        // everything else
        public long? ElapsedMilliseconds { get; set; } //only for performance entries
        public Exception Exception { get; set; } // the exception for error logging
        public string CorrelationId { get; set; } // exception shielding from server to client
        public Dictionary<string, object> AdditionalInfo { get; set; } // catch-all for anything else
    }
}
