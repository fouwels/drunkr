using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management.Instrumentation;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text.RegularExpressions;
using Demo_core.Models.DB;
using Demo_core.Repositories;
using HtmlAgilityPack;
using Microsoft.AspNet.DataProtection.Repositories;

namespace Demo_scrapers
{
	public class Program
	{
		const string RootUrl = "http://www.thedrinkshop.com/products/nlpdetail.php?prodid=";
		private ProductRepository _prodrepo;
		private int failedScrapes = 0;
		private int failedNetwork = 0;
        public async Task Main(string[] args)
		{
			_prodrepo = new Demo_core.Repositories.ProductRepository();
	        var x = _prodrepo.GetAll();

			const int startId = 300;
			const int endId = 400;
			const int batchSize = 10;
			var n = endId - startId + 1;

			var batches = new List<Tuple<int, int>>();

			for (int i = startId; i < endId; i += batchSize)
			{
				var end = (i + batchSize - 1) > endId ? endId : (i + batchSize - 1);
                batches.Add(new Tuple<int, int>(i, end));
				Debug.WriteLine("batch: " + i +" : " + (i + batchSize -1));
			}

			foreach (var batch in batches)
			{
				var Tasks = new Task[batchSize];
				for (var index = batch.Item1; index <= batch.Item2; index++)
				{
					Debug.WriteLine("scraping: " + index);
					await Scrape(index); //can async later
					//Debug.WriteLine("Adding ID: " + (index).ToString() + " to que");
					//Tasks[index - batch.Item1] = Scrape(index);
				}
				//await Task.WhenAll(Tasks);
				//Debug.WriteLine("===============\ncompleted batch " + batch.Item1 + " : " + batch.Item2 + "\n===============");
				//Debug.WriteLine("Failed:\t\t\t" + failedScrapes + "\nNetworkErrors:\t" + failedNetwork + "\n");
			}
			Debug.WriteLine("===============\nall batches completed\n===============");



			Console.ReadLine();
		}

		private async Task Scrape(int Id)
		{
			//Debug.WriteLine("scraping: " + Id);

			HttpResponseMessage r = new HttpResponseMessage();
			try
			{
				r = await GetPageEntity(id: Id.ToString());
			}
			catch
			{
				failedScrapes += 1;
				failedNetwork += 1;
				//Debug.WriteLine("[" + Id + "] FAIL: -> HTTP request failed internally?");
				return;
			}
			
			if (r.StatusCode != System.Net.HttpStatusCode.OK) { Debug.WriteLine(" <!> != 200"); }
			if (r.RequestMessage.RequestUri.AbsoluteUri.ToLower().Contains("http://www.thedrinkshop.com/notfound.php")) { Debug.WriteLine(" <!> Not Found"); return; };
			//if(Page.Contains("Please confirm you are above the legal drinking age in your country")) { Debug.WriteLine(" <!> Age gate"); return; }
			//Debug.WriteLine(""); //ew
			var Page = await r.Content.ReadAsStringAsync();


			var packdoc = new HtmlAgilityPack.HtmlDocument();
			packdoc.LoadHtml(Page);
			var doc = packdoc.DocumentNode.Descendants();

			var product = new Demo_core.Models.DB.Product();

			var tempProductName = doc
				.Where(a => a.Name == "img")
				.FirstOrDefault(x => x.Attributes.Any(b => b.OriginalName == "itemprop" && b.Value == "image") && x.Attributes.Any(z => z.OriginalName == "class" && z.Value == "mainImage"))
				?.Attributes.FirstOrDefault(y => y.OriginalName == "alt");

			if (tempProductName == null)
			{
				failedScrapes += 1;
				//Debug.WriteLine("[" + Id + "] FAIL: -> no name node");
				return;
			}
			product.Name = tempProductName.Value;


			//Debug.WriteLine("[" + Id + "] Name: " + product?.Name);

			if (string.IsNullOrEmpty(product.Name))
			{
				failedScrapes += 1;
				//Debug.WriteLine("[" + Id + "] FAIL: -> no name");
				return;
			}

			var tempDescription = doc
				.Where(a => a.Name == "span")
				.FirstOrDefault(b => b.Attributes.Any(x => x.OriginalName == "itemprop" && x.Value == "description"));

			if (tempDescription == null)
			{
				failedScrapes += 1;
				//Debug.WriteLine("[" + Id + "] FAIL: -> no description");
				return;
			}
				
			product.Description = Regex.Replace(tempDescription.InnerHtml, @"\<.+\>", string.Empty);
			//Debug.WriteLine("[" + Id + "] Description: " + product.Description);

			//<--
			var spans = doc
				.Where(a => a.Name == "span")
				.FirstOrDefault(x => x.Attributes.Any(b => b.OriginalName == "class" && b.Value == "smallHeaderText"))
				.SelectNodes("./following-sibling::text()")
				.Select(g => g.InnerHtml.Replace("&nbsp", "").Replace("\n", "").Replace(";", ""))
				.Where(y => !Regex.IsMatch(y, @"^[, ]*$") )
				.Select(p => p.Trim()).ToList();

			//product.Abv = new Abv
			//{
			//	Percentage =  float.Parse(Regex.Replace(spans[1], @"\D", string.Empty))
			//};

			//product.Producer = new Producer
			//{
			//	Name = spans[2]
			//};

			//product.CountryOfOrigin = new CountryOfOrigin
			//{
			//	Name = spans[3]
			//};
			//product.Category = new Category
			//{
			//	Name = spans[4]
			//};
			//product.Brand = new Brand
			//{
			//	Name = spans[2]
			//};
			var rg = new Regex(@"<[^>]*>");

			var bottlePropIni = doc
				.Where(x => x.Name == "table")
				.FirstOrDefault(b => b.Attributes.Any(t => t.OriginalName == "class" && t.Value == "priceTable"));

			if (bottlePropIni == null || bottlePropIni.ChildNodes.Count <=2 )
			{
				failedScrapes += 1;
				//Debug.WriteLine("[" + Id + "] FAIL: -> no bottles");
				return;
			}

			var bottleProps = bottlePropIni.ChildNodes.Where(y => y.Name == "tr").ToList()[1]
				.ChildNodes.Select(u => u.InnerHtml).ToArray().Where((val, x) => (new List<int> {0, 2, 4}).Contains(x)).ToList()
				.Select(i => rg.Replace(i, "")).ToList();

			var tempCentiliters = new float();
			if (!float.TryParse(Regex.Match(bottleProps[0], @"^\d*").Value, out tempCentiliters))
			{
				failedScrapes += 1;
				//Debug.WriteLine("[" + Id + "] FAIL: -> no bottle centiliters");
				return;
			}

			product.Bottles = new List<Bottle>();
			product.Bottles.Add(new Bottle
			{
				SizeInCentiLiters = tempCentiliters,
				RetailName = bottleProps[0],
				Price = float.Parse(Regex.Match(bottleProps[2], @"\d*$").Value)
			});

			var imageURL = (doc
				.Where(x => x.Name == "meta").ToArray()[8]).Attributes["content"].Value;

			product.Image = new Image();
			product.Image.ByteArray = await (await GetPageEntity(asset: imageURL)).Content.ReadAsByteArrayAsync();
			product.Image.FileExtension = imageURL.Split('.').Last();

			var existing = _prodrepo.GetBy(x => x.Name.ToLower() == product.Name.ToLower()).FirstOrDefault();

			if (existing == null)
			{
				Debug.WriteLine("[" + Id + "] New - Adding to datastore");
				_prodrepo.Add(product);
			}
			else
			{
				Debug.WriteLine("[" + Id + "] Existing - Updating in datastore");
				_prodrepo.Update(existing);
			}


			//var test = prodrepo.GetAll();
		}
		private async Task<HttpResponseMessage> GetPageEntity(string id = "", string asset = "") //hacky i kno
		{
			var url = asset;

			if (id != "")
			{
				url = RootUrl + id;
			}
			else
			{
				id = "ASSET";
			}


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

					HttpResponseMessage r = new HttpResponseMessage();

					var retryCounter = 0;

					Exception exHolder = new Exception();

					while (r.Content == null && retryCounter < 10)
					{
						try
						{
							r = await ht.GetAsync(url);
						}
						catch(Exception ex)
						{
							exHolder = ex;
							retryCounter += 1;
							await Task.Delay(1000);
						}
					}

					if (r.Content == null)
					{
						throw exHolder;
					}
					//Debug.Write("HttpCode: " + r.StatusCode + " on page: " + id.ToString());
					return r;
				}
			}
		}
	}
}
