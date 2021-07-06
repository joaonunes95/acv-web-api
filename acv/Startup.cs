using Database;
using Database.Context;
using Database.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace acv
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AcvContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("AcvDB")));

            services.AddControllers();
            services.AddTransient<IAudioRepository, AudioRepository>();

            //services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
            //{
            //    builder.AllowAnyHeader()
            //           .AllowAnyMethod()
            //           .SetIsOriginAllowed((host) => true)
            //           .AllowCredentials();
            //}));


            services.AddAuthentication("CookieSeriesAuth")
                //.addJWT; // mudar nome depois
                .AddCookie("CookieSeriesAuth", opt =>
                {
                    opt.Cookie.Name = "SeriesAuthCookie";
                });

            services.AddAuthorization(opt => {
                opt.AddPolicy("Admin", p => p.RequireRole("SecretRole"));

                opt.AddPolicy("AdvancedUser", p => p.RequireRole("Student"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
