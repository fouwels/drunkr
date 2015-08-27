using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Demo_scrapers
{
    public class Program
    {
        public void Main(string[] args)
        {
			const int startId = 4499;
			const int endId = 5499;

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
			Debug.WriteLine("scraping: " + Id.ToString());
		}
    }
}
