using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

namespace Demo.Models.DB
{
    public class DatabaseContext : DbContext
    {
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
		}

		public DbSet<Bottle> Bottles { get; set; }
		public DbSet<Manufacturer> Manufacturers
		{
			get; set;
		}
		public DbSet<Spirit> Spirits
		{
			get; set;
		}
    }
}