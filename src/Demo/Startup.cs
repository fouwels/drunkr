using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Data.Entity;
using Microsoft.Framework.ConfigurationModel;
using Demo.Models.DB;

namespace Demo
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
        }

        // This method gets called by a runtime.
        // Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
			var config = new Configuration()
				.AddJsonFile("config.json");

			var connectionString = config["Data:ConnectionStrings:DefaultConnection"];

			if (connectionString == "") { throw new KeyNotFoundException("Connection String not found in config.json"); }

            services.AddMvc();
			services.AddEntityFramework()
				.AddSqlServer()
				.AddDbContext<DatabaseContext>(x => x.UseSqlServer(connectionString));
        }
		
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Configure the HTTP request pipeline.
            app.UseStaticFiles();

            // Add MVC to the request pipeline.
            app.UseMvc();
            // Add the following route for porting Web API 2 controllers.
            // routes.MapWebApiRoute("DefaultApi", "api/{controller}/{id?}");

			app.UseStaticFiles
        }
    }
}
