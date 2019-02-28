using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace TraceAS
{
    public class IpInfo
    {
        [JsonProperty("ip")] public string Ip { get; set; }

        [JsonProperty("hostName")] public string HostName { get; set; }

        [JsonProperty("city")] public string City { get; set; }

        [JsonProperty("region")] public string Region { get; set; }

        [JsonProperty("loc")] public string Location { get; set; }

        [JsonProperty("postal")] public string Postal { get; set; }

        [JsonProperty("phone")] public string Phone { get; set; }

        [JsonProperty("org")] public string Organization { get; set; }

        [JsonProperty("country")] public string Country { get; set; }

        public override string ToString()
        {
            return $"Ip: {Ip}\n" +
                   $"HostName: {HostName}\n" +
                   $"City: {City}\n" +
                   $"Region: {Region}\n" +
                   $"Country: {Country}\n" +
                   $"Location: {Location}\n" +
                   $"Postal: {Postal}\n" +
                   $"Phone: {Phone}\n" +
                   $"Organization: {Organization}\n";
        }

        public string PrintInfo()
        {
            var result = string.Empty;

            GetType().GetTypeInfo()
                .DeclaredProperties
                .Where(x => x.GetValue(this) != null)
                .Select(x => $"{x.Name}: {x.GetValue(this)}")
                .ToList()
                .ForEach(x => result += x);
//                .Select(item => result += item + "\n");

            return result;
        }
    }
}