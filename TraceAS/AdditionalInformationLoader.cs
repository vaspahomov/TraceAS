using System.IO;
using System.Net;
using System.Text;

namespace TraceAS
{
    public class AdditionalInformationLoader
    {
        public static string GetInformation(string ipAddress)
        {
            var request = (HttpWebRequest) WebRequest.Create($"http://ipinfo.io/{ipAddress}/json");

            var resp = request.GetResponse() as HttpWebResponse;
            //TODO обработать плохие ответы сервера 
            var text = "";
            using (var sr = new StreamReader(resp.GetResponseStream(), Encoding.GetEncoding(1251)))
            {
                text = sr.ReadToEnd();
            }

            text = text.Trim();
            return text;
        }
    }
}