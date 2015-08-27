using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using HtmlAgilityPack;

namespace Demo_scrapers
{
    public class Program
    {
		const string RootUrl = @"http://www.thedrinkshop.com/products/productlist.php?prodid=";
        public async void Main(string[] args)
        {
			const int startId = 244;
			const int endId = 244;

			var n = endId - startId + 1;

			var Tasks = new Task[n];

			await Scrape(244);


			//for (var index = startId; index <= endId; index++)
			//{
			//Tasks[index - startId] = Scrape(index);
			//}

			//Task.WaitAll(Tasks);
			Console.ReadLine();
        }
		public async Task Scrape(int Id)
		{
			try
			{
				Debug.WriteLine("scraping: " + Id.ToString());

				var Page = "";

				using (var handler = new HttpClientHandler())
				{
					handler.CookieContainer = new System.Net.CookieContainer();
					handler.CookieContainer.Add(new System.Net.Cookie("age_gate", "legal", "/", "www.thedrinkshop.com"));

					using (var ht = new HttpClient(handler))
					{
						var r = await ht.GetAsync(RootUrl + Id.ToString());
						Debug.Write(r.StatusCode + " on page: " + Id.ToString());
						if (r.StatusCode != System.Net.HttpStatusCode.OK) { Debug.WriteLine(" <!>"); return; }
						if (r.RequestMessage.RequestUri.AbsoluteUri.ToLower().Contains("http://www.thedrinkshop.com/notfound.php")) { Debug.WriteLine(" <!>"); return; };
						Debug.WriteLine("");

						Page = await r.Content.ReadAsStringAsync();
					}
				}

				var packdoc = new HtmlAgilityPack.HtmlDocument();
				packdoc.LoadHtml(Page);
				var doc = packdoc.DocumentNode.Descendants();
				var span = doc.Where(a => a.Name == "span");
				var t = span.Where(x => x.Attributes.Any(q => q.Value.ToLower() != "divider"));
			}
			catch (Exception ex)
			{

				throw; //breakoint and catch while in async
			}
        }
    }
}
