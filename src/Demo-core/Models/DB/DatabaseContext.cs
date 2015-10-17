using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.Runtime;

namespace Demo_core.Models.DB
{
    public class DatabaseContext : DbContext
    {
		protected override void OnModelCreating(ModelBuilder builder)
		{
			//builder.Entity<Product>().
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			//var connString =
			//	"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=F:\\Workbench\\Demo-Api\\Demo\\src\\Demo-core\\App_Data\\DatabaseContext.mdf;Integrated Security=True;Connect Timeout=30";
      optionsBuilder.UseSqlServer(connString);

      var connString = "
      Data Source=tcp:j5is8c9imd.database.windows.net,1433;
      Database=staging;
      User ID=combodb@j5is8c9imd;
      Password=6drMQg3jgP6ywt55efdnekmk;
      Trusted_Connection=False;
      Encrypt=True;
      Connection Timeout=30;"

      //commiting password ay, server IS firewalled so yolo

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
