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
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using AutoMapper;
using DIO.GameCatalog.AutoMapper;
using Microsoft.EntityFrameworkCore;
using DIO.GameCatalog.Context;
using DIO.GameCatalog.Services.V1;
using DIO.GameCatalog.Services.V1.Interfaces;
using DIO.GameCatalog.Repositories.Interfaces;
using DIO.GameCatalog.Repositories;
using System.Reflection;
using System.IO;
using DIO.GameCatalog.Middlewares;

namespace DIO.GameCatalog
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
            services.AddDbContext<GameDbContext>(options =>
                options.UseNpgsql(
                    Configuration.GetConnectionString("DefaultConnection")
                )
            );

            services.AddControllers();
            services.AddSwaggerGen(
                c =>
                {
                    c.SwaggerDoc(
                        "v1",
                        new OpenApiInfo
                        {
                            Title = "DIO Game Catalog",
                            Version = "v1"
                        }
                    );

                    var basePath = AppDomain.CurrentDomain.BaseDirectory;

                    var fileName = typeof(Startup)
                        .GetTypeInfo()
                        .Assembly
                        .GetName()
                        .Name + ".xml";

                    c.IncludeXmlComments(Path.Combine(basePath, fileName));
                }
            );

            services.AddAutoMapper(typeof(Startup));

            services.AddScoped<IGamesService, GamesService>();
            services.AddScoped<IGamesRepository, GamesRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(
                    c =>
                    {
                        c.SwaggerEndpoint(
                            "/swagger/v1/swagger.json",
                            "dio.game_catalog v1"
                        );
                        c.RoutePrefix = string.Empty;
                    }
                );
            }

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(
                endpoints =>
                {
                    endpoints.MapControllers();
                }
            );
        }
    }
}
