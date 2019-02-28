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
                    foreach (var ip in TraceRoute.GetTraceRoute(o.Addres))
                    {
                        var ipInfoJson = AdditionalInformationLoader.GetInformation(ip.ToString());
                        var ipInfo = JsonConvert.DeserializeObject<IpInfo>(ipInfoJson);                
                        Console.WriteLine(ipInfo);
                    }
                });
        }

        private class Options
        {
            [Value(1, MetaName = "address", HelpText = "Enter Ip address or domain name of the host", Required = true)]
            public string Addres { get; set; }
        }
    }
}