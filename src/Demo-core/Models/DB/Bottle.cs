using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo_core.Models.DB
{
    public class Bottle : Base
    {
		public float QuantityMl
		{
			get; set;
		}
		public virtual Liquid Liquid
		{
			get; set;
		}
		public virtual Manufacturer Manufacturer //redundant, I knoe
		{
			get; set;
		}
    }
}
