using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Data.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Google.Apis.Auth.AspNetCore3;
using Helpers.Handlers;
using Repository;
using Repository.Interfaces;

namespace IceCreamTrackerApi
{
    public class Startup
    {
        readonly string CORS_POLICY = "CorsPolicy";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = null); //this returns PascalCase but not sure if it is the right way to go about it
            services.AddCors(options =>
            {
                options.AddPolicy(name: CORS_POLICY, builder => 
                {
                    builder.WithOrigins("http://localhost:4200", "https://scoopreviewdude.netlify.app").AllowAnyHeader().AllowAnyMethod();
                });
            });

            services.AddDbContext<IceCreamDataContext>(options =>
                { 
                    options.UseSqlServer(Configuration.GetConnectionString("DatabaseConnection"));
                    options.UseLazyLoadingProxies();
                
                });

            services.AddDbContext<IceCreamDataContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseSqlServer(Configuration.GetConnectionString("DatabaseConnection"));


            });

            services.AddAuthentication(o =>
            {
                o.DefaultChallengeScheme = GoogleOpenIdConnectDefaults.AuthenticationScheme;
                o.DefaultForbidScheme = GoogleOpenIdConnectDefaults.AuthenticationScheme;
            })
                .AddGoogleOpenIdConnect(options => 
                {
                    options.ClientId = Configuration["IceCreamTrackerApi:GoogleApiKey"];
                });
            
            
            // DI TODO: add interfaces to other repos for proper DI set up
            services.AddScoped<JwtHandler>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IIceCreamRepository, IceCreamRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(CORS_POLICY);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
