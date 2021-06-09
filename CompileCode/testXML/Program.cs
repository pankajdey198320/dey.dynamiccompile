using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using System.Xml.Serialization;
namespace testXML
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Task> ta = new List<Task>();
            for (var i = 0; i < 10; i++)
            {
                Task t = ReadAsync("http://sxp.microsoft.com/feeds/msdntn/VisualStudioNews");
                ta.Add(t);
            }
            Task.WaitAll(ta.ToArray());

            Console.ReadKey();
        }

        static async Task<rss> ReadAsync(string url)
        {

            using (var httpResponse = await new HttpClient().GetAsync(url)
                .ConfigureAwait(continueOnCapturedContext: false)) //IO-bound
            using (var responseContent = httpResponse.Content)
            using (var contentStream = await responseContent.ReadAsStreamAsync()
                .ConfigureAwait(continueOnCapturedContext: false)) //IO-bound
                return LoadXmlDocument(contentStream); //CPU-bound
        }

        static rss LoadXmlDocument(Stream st)
        {
            XmlSerializer sl = new XmlSerializer(typeof(rss));
            return sl.Deserialize(st) as rss;
        }
    }
}
