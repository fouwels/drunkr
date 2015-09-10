using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Demo_core.Models.DB
{
    public class Bottle : Base
    {
		public string RetailName { get; set; }
		public float SizeInCentiLiters { get; set; }
		public float Price { get; set; }
	}
}
