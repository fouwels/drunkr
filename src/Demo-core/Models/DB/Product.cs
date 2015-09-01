using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Demo_core.Models.DB
{
    public class Product : Base
    {
		[Required]
		public string Name { get; set; }
		public string Description { get; set; }
		public virtual Image Image { get; set; }
		public virtual Producer Producer { get; set; }
		public virtual Grade Grade { get; set; }
		public virtual CountryOfOrigin CountryOfOrigin { get; set; }
		public virtual Abv Abv { get; set; }
		public virtual Brand Brand { get; set; }
		public virtual Category Category { get; set; }
		public virtual List<Bottle> Bottles { get; set; }
		public virtual List<Review> Reviews { get; set; }
	}
}
