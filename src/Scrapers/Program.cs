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
		const string RootUrl = @"http://www.thedrinkshop.com/products/productlist.php?catid=";
        public void Main(string[] args)
        {
			const int startId = 244;
			const int endId = 244;

			var n = endId - startId + 1;

			var Tasks = new Task[n];

			for (var index = startId; index <= endId; index++)
			{
				Tasks[index - startId] = Scrape(index);
			}

			Task.WaitAll(Tasks);
			Console.ReadLine();
        }
		public async Task Scrape(int Id)
		{
			//Debug.WriteLine("scraping: " + Id.ToString());

			var Page = "";

			using (var ht = new HttpClient())
			{
				var r = (await ht.GetAsync(RootUrl + Id.ToString()));
				Debug.Write(r.StatusCode + " on page: " + Id.ToString());
				if (r.StatusCode != System.Net.HttpStatusCode.OK) { Console.WriteLine(" <!>"); return; }
				Console.WriteLine();

                Page = await r.Content.ReadAsStringAsync();
			}

			var packdoc = new HtmlAgilityPack.HtmlDocument();
			packdoc.Load(Page);



		}
    }
}
