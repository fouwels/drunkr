using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo_core.Models.DB
{
    public class Image : Base
    {
		public byte[] ByteArray { get; set; }
		public string FileExtension { get; set; }
	}
}
