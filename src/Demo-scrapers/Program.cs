using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management.Instrumentation;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using HtmlAgilityPack;

namespace Demo_scrapers
{
	public class Program
	{
		private const bool Verbose = false;
		const string RootUrl = "http://www.thedrinkshop.com/products/nlpdetail.php?prodid=";
		public async Task Main(string[] args)
		{
			const int startId = 244;
			const int endId = 244;

			var n = endId - startId + 1;

			var Tasks = new Task[n];
			for (var index = startId; index <= endId; index++)
			{
				Debug.WriteLine("Adding ID: " + (index).ToString() + " to que");
				Tasks[index - startId] = Scrape(index);
				

			}

			Task.WaitAll(Tasks);
			Console.ReadLine();
		}
		public async Task Scrape(int Id)
		{
			Debug.WriteLine("scraping: " + Id.ToString());
			var Page = "";

			using (var handler = new HttpClientHandler())
			{
				handler.CookieContainer = new System.Net.CookieContainer();
				handler.UseCookies = true;
				handler.AllowAutoRedirect = true;

				var agecookie = new System.Net.Cookie("age_gate", "legal", "/", "www.thedrinkshop.com");
				agecookie.Expires = DateTime.Now.AddYears(1);

				var sessioncookie = new System.Net.Cookie("PHPSESSID", "uf5mf0n7o4m2ldui2rrual7tt6", "/", "www.thedrinkshop.com");

                handler.CookieContainer.Add(agecookie);
				handler.CookieContainer.Add(sessioncookie);

				using (var ht = new HttpClient())
				{
					ht.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)");

					var r = await ht.GetAsync(RootUrl + Id.ToString());
					Debug.Write("HttpCode: " + r.StatusCode + " on page: " + Id.ToString());
					if (r.StatusCode != System.Net.HttpStatusCode.OK) { Debug.WriteLine(" <!> != 200"); return; }
					if (r.RequestMessage.RequestUri.AbsoluteUri.ToLower().Contains("http://www.thedrinkshop.com/notfound.php")) { Debug.WriteLine(" <!> Not Found"); return; };

					Page = await r.Content.ReadAsStringAsync();
					//if(Page.Contains("Please confirm you are above the legal drinking age in your country")) { Debug.WriteLine(" <!> Age gate"); return; }

					Debug.WriteLine("");

				}
			}

			var packdoc = new HtmlAgilityPack.HtmlDocument();
			packdoc.LoadHtml(Page);
			//var doc = packdoc.DocumentNode.Descendants();
			var desc = packdoc.DocumentNode.SelectNodes("//*[@id=\"middle-column\"]/div/div[1]/div[1]/div[3]/div[1]/div/span[2]");
			//var span = doc.Where(a => a.Name == "span");
			//var t = span.Where(x => x.Attributes.Any(q => q.Value.ToLower() != "divider"));
		}
    }
}
