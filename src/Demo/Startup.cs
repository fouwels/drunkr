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
using Microsoft.Framework.Configuration;
using Demo.Models.DB;
using Microsoft.Framework.Runtime;
using Microsoft.AspNet.Diagnostics;

namespace Demo
{
    public class Startup
    {
		IConfiguration Config { get; set; }
		public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
		{
			var builder = new ConfigurationBuilder(appEnv.ApplicationBasePath)
				.AddJsonFile("config.json")
				.AddJsonFile($"config.{env.EnvironmentName}.json", optional: true);

			if (env.IsDevelopment())
			{
				builder.AddUserSecrets();
			}
			builder.AddEnvironmentVariables();
			Config = builder.Build();
		}
		
		public void ConfigureServices(IServiceCollection services)
        {
			var connectionString = Config["Data:ConnectionStrings:DefaultConnection"];
			if (connectionString == "") { throw new KeyNotFoundException("Connection String not found in config.json"); }

            services.AddMvc();
			services.AddEntityFramework()
				.AddSqlServer()
				.AddDbContext<DatabaseContext>(x => x.UseSqlServer(connectionString));
        }
		
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();
			app.UseErrorPage();
            app.UseMvc();
			app.UseStaticFiles();
        }
    }
}
