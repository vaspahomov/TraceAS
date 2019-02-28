using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

namespace TraceAS
{
    public class TraceRoute
    {
        public static IEnumerable<IPAddress> GetTraceRoute(
            string hostname, 
            int maxTTL = 30, 
            string data = "", 
            int timeout = 10000)
        {
            var buffer = Encoding.ASCII.GetBytes(data);
            var pinger = new Ping();

            for (var ttl = 1; ttl <= maxTTL; ttl++)
            {
                var options = new PingOptions(ttl, true);
                var reply = pinger.Send(hostname, timeout, buffer, options);

                switch (reply.Status)
                {
                    case IPStatus.Success:
                        // Success means the tracert has completed
                        yield return reply.Address;
                        break;
                    case IPStatus.TtlExpired:
                        // TtlExpired means we've found an address, but there are more addresses
                        yield return reply.Address;
                        continue;
                    case IPStatus.TimedOut:
                        continue;
                }

                // if we reach here, it a status we don't recognize and we should exit.
                break;
            }
        }
    }
}