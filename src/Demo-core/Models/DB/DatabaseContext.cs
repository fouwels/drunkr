﻿using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Framework.Configuration;
namespace Demo_core.Models.DB
{
    public class DatabaseContext : DbContext
    {
		public DatabaseContext()
		{
			var builder = new ConfigurationBuilder(@".\")
				.AddJsonFile("Config.json");

			Configuration = builder.Build();
			var watch = Configuration["Data:ConnectionStrings:DefaultConnection"];
        }
		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<Abv>().Key(e => e.Id);
			base.OnModelCreating(builder);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			var connString = Configuration["Data:ConnectionStrings:DatabaseContext"];
			optionsBuilder.UseSqlServer(connString);
		}

		private IConfiguration Configuration;

		public DbSet<Abv> Abvs { get; set; }
		public DbSet<Base> Bases { get; set; }
		public DbSet<Bottle> Bottles { get; set; }
		public DbSet<Brand> Brands { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<CountryOfOrigin> CountriesOfOrigin { get; set; }
		public DbSet<Grade> Grades { get; set; }
		public DbSet<Image> Images { get; set; }
		public DbSet<Producer> Producers { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Review> Reviews { get; set; }
	}
}
