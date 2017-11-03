using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AlpineSkiHouse.Web.Data;
using AlpineSkiHouse.Web.Models;
using AlpineSkiHouse.Web.Services;
using AlpineSkiHouse.Web.Configuration;
using AlpineSkiHouse.Web.Configuration.Models;
using Serilog;

namespace AlpineSkiHouse.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddCsrInformationFile("config\\TCP_ONLINE_AGENTS.INFO", reloadOnChange: true)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see https://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<ApplicationUserContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<SkiCardContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<PassTypeContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<PassContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.Configure<CsrInformationOptions>(Configuration.GetSection("CsrInformationOptions"));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationUserContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // Console logging
            // Uncomment to use the default console logger
            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //loggerFactory.AddDebug();

            //loggerFactory.AddDebug((className, logLevel) =>
            //{
            //    if (className.StartsWith("AlpinkeSkiHouse."))
            //    {
            //        return true;
            //    }

            //    return false;
            //});

            // Serilog config
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("AlpineSkiHouse", Serilog.Events.LogEventLevel.Debug)
                .Enrich.FromLogContext()
                .WriteTo.LiterateConsole()
                .WriteTo.Seq("http://localhost:5341")
                .CreateLogger();

            loggerFactory.AddSerilog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseIdentity();

            if (Configuration["Authentication:Facebook:AppId"] == null ||
                Configuration["Authentication:Facebook:AppSecret"] == null ||
                Configuration["Authentication:Twitter:ConsumerKey"] == null ||
                Configuration["Authentication:Twitter:ConsumerSecret"] == null)
            {
                throw new KeyNotFoundException("A configuration value is missing for authentication against Facebook and Twitter. While you don't need to " +
                    "get tokens for these you do need to set up your user secrets as described in the readme.");

                app.UseFacebookAuthentication(new FacebookOptions
                {
                    AppId = Configuration["Authentication:Facebook:AppId"],
                    AppSecret = Configuration["Authentication:Facebook:AppSecret"]
                });

                app.UseTwitterAuthentication(new TwitterOptions
                {
                    ConsumerKey = Configuration["Authentication:Twitter:ConsumerKey"],
                    ConsumerSecret = Configuration["Authentication:Twitter:ConsumerSecret"]
                });
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
