using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Models.DB
{
    public class Liquid : Base
    {
		public string Name { get; set; }
		public float ABV { get; set; }

		public virtual Spirit Spirit
		{
			get; set;
		}

		public virtual Manufacturer Manufacturer
		{
			get; set;
		}


    }
}
