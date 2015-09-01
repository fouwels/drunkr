using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo_core.Models;
using Demo_core.Models.DB;
using Microsoft.AspNet.Http.Internal;

namespace Demo_core.Repositories
{
    public class ProductRepository : GenericRepository<Product>
	{
		private DatabaseContext DataContext = new DatabaseContext();
		//public Product Add(Product Item)
		//{
		//	var a = DataContext.Products;
		//	a.Add(Item);
		//	DataContext.SaveChanges();
		//	return Item;
		//}
	}
}
