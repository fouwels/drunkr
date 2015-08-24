using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Demo_core.Models.DB;
using Demo_core.Repositories;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Demo_api.Api.v0.Controllers
{
	[Route("api/v0/[controller]/[action]")]
	public class ProductsController : Controller
	{
		[HttpGet]
		public IEnumerable<Product> Get(ProductRepository spiritRepository)
		{
			return spiritRepository.GetBy(x => x.Name == "yolo");
		}

		[HttpGet]
		public Product Add(ProductRepository spiritRepository)
		{
			var product = new Product
			{
				Name = "yeee"
			};
			var x = spiritRepository.Add(product);
			return x;
		}
	}
}
