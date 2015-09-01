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
using HtmlAgilityPack;
using Microsoft.AspNet.DataProtection.Repositories;

namespace Demo_scrapers
{
	public class Program
	{
		private const bool Verbose = false;
		const string RootUrl = "http://www.thedrinkshop.com/products/nlpdetail.php?prodid=";
		public async Task Main(string[] args)
		{
			const int startId = 11128;
			const int endId = 11128;

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

		private async Task Scrape(int Id)
		{
			Debug.WriteLine("scraping: " + Id);

			var r = await GetPageEntity(id: Id.ToString());
			if (r.StatusCode != System.Net.HttpStatusCode.OK) { Debug.WriteLine(" <!> != 200"); }
			if (r.RequestMessage.RequestUri.AbsoluteUri.ToLower().Contains("http://www.thedrinkshop.com/notfound.php")) { Debug.WriteLine(" <!> Not Found"); return; };
			//if(Page.Contains("Please confirm you are above the legal drinking age in your country")) { Debug.WriteLine(" <!> Age gate"); return; }
			Debug.WriteLine(""); //ew
			var Page = await r.Content.ReadAsStringAsync();


			var packdoc = new HtmlAgilityPack.HtmlDocument();
			packdoc.LoadHtml(Page);
			var doc = packdoc.DocumentNode.Descendants();

			var product = new Demo_core.Models.DB.Product();

			product.Name = doc
				.Where(a => a.Name == "img")
				.FirstOrDefault(x => x.Attributes.Any(b => b.OriginalName == "itemprop" && b.Value == "image") && x.Attributes.Any(z => z.OriginalName == "class" && z.Value == "mainImage"))
				.Attributes.FirstOrDefault(y => y.OriginalName == "alt").Value;
			Debug.WriteLine("[" + Id + "] Name: " + product?.Name);

			product.Description = Regex.Replace(doc
				.Where(a => a.Name == "span")
				.FirstOrDefault(b => b.Attributes.Any(x => x.OriginalName == "itemprop" && x.Value == "description"))
				.InnerHtml, @"\<.+\>", string.Empty);
			//Debug.WriteLine("[" + Id + "] Description: " + product.Description);

			//<--
			var spans = doc
				.Where(a => a.Name == "span")
				.FirstOrDefault(x => x.Attributes.Any(b => b.OriginalName == "class" && b.Value == "smallHeaderText"))
				.SelectNodes("./following-sibling::text()")
				.Select(g => g.InnerHtml.Replace("&nbsp", "").Replace("\n", "").Replace(";", ""))
				.Where(y => !Regex.IsMatch(y, @"^[, ]*$") )
				.Select(p => p.Trim()).ToList();

			product.Abv = new Abv
			{
				Percentage =  float.Parse(Regex.Replace(spans[1], @"\D", string.Empty))
			};

			product.Producer = new Producer
			{
				Name = spans[2]
			};

			product.CountryOfOrigin = new CountryOfOrigin
			{
				Name = spans[3]
			};
			product.Category = new Category
			{
				Name = spans[4]
			};
			product.Brand = new Brand
			{
				Name = spans[2]
			};
			var rg = new Regex(@"<[^>]*>");

			var bottleProps = doc
				.Where(x => x.Name == "table")
				.FirstOrDefault(b => b.Attributes["class"].Value == "priceTable")
				.ChildNodes.Where(y => y.Name == "tr").ToList()[1]
				.ChildNodes.Select(u => u.InnerHtml).ToArray().Where((val, x) => (new List<int> {0, 2, 4}).Contains(x)).ToList()
				.Select(i => rg.Replace(i, "")).ToList();

			product.Bottles = new List<Bottle>();
			product.Bottles.Add(new Bottle
			{
				SizeInCentiLiters = float.Parse(Regex.Match(bottleProps[0], @"^\d*").Value),
				RetailName = bottleProps[0],
				Price = float.Parse(Regex.Match(bottleProps[2], @"\d*$").Value)
			});

			var imageURL = (doc
				.Where(x => x.Name == "meta").ToArray()[8]).Attributes["content"].Value;

			product.Image = new Image();
			product.Image.ByteArray = await (await GetPageEntity(asset: imageURL)).Content.ReadAsByteArrayAsync();
			product.Image.FileExtension = imageURL.Split('.').Last();

			var prodrepo = new Demo_core.Repositories.ProductRepository();

			var existing = prodrepo.GetBy(x => x.Name.ToLower() == product.Name.ToLower()).FirstOrDefault();

			if (existing == null)
			{
				prodrepo.Add(product);
			}
			else
			{
				prodrepo.Update(existing);
			}
			

			var test = prodrepo.GetAll();
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

					var r = await ht.GetAsync(url);
					Debug.Write("HttpCode: " + r.StatusCode + " on page: " + id.ToString());
					return r;
				}
			}
		}
	}
}
