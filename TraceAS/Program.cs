using System;
using CommandLine;
using Newtonsoft.Json;

namespace TraceAS
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(o =>
                {
                    foreach (var ip in TraceRoute.GetTraceRoute(o.Address, o.MaxTtl, o.Data, o.Timeout))
                    {
                        var ipInfoJson = AdditionalInformationLoader.GetInformation(ip.ToString());
                        var ipInfo = JsonConvert.DeserializeObject<IpInfo>(ipInfoJson);
                        Console.WriteLine(ipInfo.PrintInfo());
                    }
                });
        }

        private class Options
        {
            [Value(1, MetaName = "address", HelpText = "Ip address or domain name of the host", Required = true)]
            public string Address { get; set; }

            [Option("ttl", HelpText = "Max TTL", Default = 30)]
            public int MaxTtl { get; set; }

            [Option("data", HelpText = "Payload of arp request", Default = "")]
            public string Data { get; set; }

            [Option("timeout ", HelpText = "Timeout in milliseconds", Default = 10000)]
            public int Timeout { get; set; }
        }
    }
}