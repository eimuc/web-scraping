using HtmlAgilityPack;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Scraping
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var httpClient = new HttpClient();
                var url = "https://www.cvbankas.lt/?miestas=Vilnius&padalinys%5B%5D=76&keyw=";
                var response = await httpClient.GetAsync(url);
                var responseBody = await response.Content.ReadAsStringAsync();

                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(responseBody);

                var nodes = htmlDoc.DocumentNode.Descendants("h3")
                    .Where(node => node.GetAttributeValue("class", "").Contains("list_h3")).ToList()
                    .Select(node => node.InnerText);

                foreach (var node in nodes)
                {
                    Console.WriteLine(node);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
