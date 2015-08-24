using Demo_core.Models.DB;
using Microsoft.Framework.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo_core
{
    public static class Startup
    {
        public static void Configure()
        {
			var databaseContext = new DatabaseContext();
        }
    }
}
