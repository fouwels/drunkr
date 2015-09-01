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
using Demo_core.Models.DB;
using Microsoft.Framework.Runtime;
using Microsoft.AspNet.Diagnostics;
using Microsoft.Framework.Logging;
using Demo_core.Repositories;

namespace Demo_api
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
			//var connectionString = Config["Data:ConnectionStrings:DefaultConnection"];
			//if (connectionString == null) { throw new KeyNotFoundException("Connection String not found in config.json"); }

			services.AddLogging();
			//var lfactory = new LoggerFactory(); //can't take from DI yet :'(
			//var logger = lfactory.CreateLogger("ConfigureServices");
			//lfactory.AddConsole(LogLevel.Information); //<----
			//logger.LogInformation("Using connectionString:\n" + connectionString + "\n");

			services.AddMvc();
			
			//services.AddScoped<BottleRepository>();
			//services.AddScoped<LiquidRepository>();
			//services.AddScoped<ManufacturerRepository>();
			//services.AddScoped<SpiritRepository>();
		}
		
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory LoggerFactory, ILogger<Startup> logger)
        {
			LoggerFactory.AddConsole(LogLevel.Information);

			app.Use(async (context, next) =>
			{
				var s = ("[Pipeline0] Request to:" + context.Request.Path);
				logger.LogInformation(s);
				await next();
			});
			app.UseStaticFiles();
			app.UseErrorPage();
            app.UseMvc();
        }
    }
}
