using System.Net;
using CobranzaAPI.Core.Exceptions;
using CobranzaAPI.Core.Interfaces;
using CobranzaAPI.Core.Services;
using CobranzaAPI.Persistence;
using CobranzaAPI.Persistence.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;

namespace CobranzaAPI
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
            services.AddDbContext<CobranzaContext>(opt =>
                opt.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
                
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Info { Title = "CobranzaAPI", Version = "v1"}); });

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IClienteService, ClienteService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "CobranzaAPI v1"); });

            app.UseExceptionHandler(a => a.Run(async context => {
                var feature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = feature.Error;
                var exceptionType = exception.GetType();
                var status = HttpStatusCode.InternalServerError;
                string result = string.Empty;

                if (exceptionType == typeof(DbUpdateException))
                {
                    status = HttpStatusCode.InternalServerError;
                    result = "{\"title\":\"Se produjo un error en la BD de la aplicación!\", \"status\":500}";
                }
                else if (exceptionType == typeof(DbUpdateConcurrencyException))
                {
                    status = HttpStatusCode.NotFound;
                    result = "{\"title\":\"Información no encontrada!\", \"status\":404}";
                }
                else if (exceptionType == typeof(AppValidationException))
                {
                    status = ((AppValidationException)exception).StatusCode;
                    result = JsonConvert.SerializeObject(new { errors = ((AppValidationException)exception).Failures, title = exception.Message, status = (int)status });
                }
                else if (exception is AppException)
                {
                    status = ((AppException)exception).StatusCode;
                    result = JsonConvert.SerializeObject(new { title = exception.Message, status = (int)status });
                }
                else
                {
                    result = "{\"title\":\"Se produjo un error en la aplicación!\", \"status\":500}";
                }

                context.Response.ContentType = "application/problem+json";
                context.Response.StatusCode = (int)status;
                await context.Response.WriteAsync(result);
            }));
            app.UseMvc();
        }
    }
}
