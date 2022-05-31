using api_loja.Data;
using api_loja.Services;
using api_loja.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace api_loja
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
            services.Configure<FormOptions>(options =>
            {
                options.ValueLengthLimit = int.MaxValue;
                options.MultipartBodyLengthLimit = int.MaxValue; // if don't set 
                                                                 //default value is: 128 MB
                options.MultipartHeadersLengthLimit = int.MaxValue;
            });

            string db = Configuration.GetConnectionString("DB");
            if(db == "mssql")
            {
                string msSqlConnection = Configuration.GetConnectionString("MsSqlConnection");
                services.AddDbContextPool<AppDbContext>(options => {
                    options.UseSqlServer(msSqlConnection);
                    options.UseLazyLoadingProxies();
                });
            }
            else if(db == "mysql")
            {
                string mySqlConnection = Configuration.GetConnectionString("MySqlConnection");
                services.AddDbContext<AppDbContext>(options => {
                    options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection));
                    options.UseLazyLoadingProxies();
                });
            }
            services.AddControllers();
            services.AddTransient<IAuthService, AuthService>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { 
                    Title = "api_loja", 
                    Version = "v1", 
                    Description = "API para manipulação de dados referentes a produtos e categorias",
                    Contact = new OpenApiContact()
                    {
                        Name = "Emerson",
                        Email = "emersondejesussantos@hotmail.com",
                        Url = new Uri("https://www.linkedin.com/in/emerson-de-jesus-santos-303640195/")
                    },
                });

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "api_loja v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
