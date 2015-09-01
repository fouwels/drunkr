using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Demo_core.Models.DB
{
    public class Base
    {
		[Key]
		public Guid Id { get; set; }
	}
}
