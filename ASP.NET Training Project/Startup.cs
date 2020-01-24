using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP.NET_Training_Project.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using System.Data.SqlClient;

namespace ASP.NET_Training_Project
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication("TestAuth")
                .AddCookie("TestAuth", config => 
                { 
                
                });

            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<IdentityDataContext>();

            //This protects against excessive authentication attempts
            services.Configure<IdentityOptions>(options => 
            {
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
                //password settings - these are quite lenient for testing purposes
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 5;
            });
            
            services.AddTransient<FormattingService>();

            services.AddTransient<FeatureToggles>(x => new FeatureToggles { 
                DeveloperExceptions = configuration.GetValue<bool>("FeatureToggles:DeveloperExceptions")
            });
            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });

            services.AddDbContext<IdentityDataContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("IdentityDataContext");
                options.UseSqlServer(connectionString);
            });

            services.AddDbContext<BlogDataContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("BlogDataContext");
                options.UseSqlServer(connectionString);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, FeatureToggles features)
        {
            app.UseExceptionHandler("/error.html");

            if (features.DeveloperExceptions)
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();         
            
            app.Use(async (context, next) =>
            {
                if (context.Request.Path.Value.Contains("invalid"))
                    throw new Exception("ERROR!");

                await next();
            }
            );

            //Not neccessary if deplyoying with IIS
            /*
            app.UseForwardedHeaders(new ForwardedHeadersOptions 
            { 
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            */
            app.UseAuthentication();
            

            app.UseMvc(routes => {
                routes.MapRoute("Default", "{controller=Home}/{action=Index}/{id:int?}");
            });

            app.UseFileServer();
        }
    }
}
