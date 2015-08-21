using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

namespace Demo.Models.DB
{
    public class DatabaseContext : DbContext
    {
		
		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<Bottle>().Key(x => x.Id);
			builder.Entity<Manufacturer>().Key(x => x.Id);
			builder.Entity<Spirit>().Key(x => x.Id);
			builder.Entity<Liquid>().Key(x => x.Id);
		}

		public DbSet<Bottle> Bottles { get; set; }
		public DbSet<Manufacturer> Manufacturers{get; set;}
		public DbSet<Spirit> Spirits{get; set;}
		public DbSet<Liquid> Liquids{get; set;}
    }
}