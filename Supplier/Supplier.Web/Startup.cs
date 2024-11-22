using HealthChecks.UI.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Supplier.Application.Services;
using Supplier.Domain.Interfaces;
using Supplier.Infra.Context;
using Supplier.Infra.Repository;

namespace Supplier.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddMvc(options => options.EnableEndpointRouting = false);

            services.AddDbContext<MainContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("SupplierConnectionString")));
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddCors();

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Supplier", Version = "v1" }); });
            
            services.AddHealthChecks(); 
            //Configurando o Health Ckeck
            services.ConfigureHealthChecks(Configuration); 

            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<MainContext>();
                if (context.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
                {
                    context.Database.Migrate();
                }
            }

            //HealthCheck Middleware
            app.MapHealthChecks("/health", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = async (context, report) =>
                {
                    context.Response.ContentType = "application/json";
                    var result = JsonSerializer.Serialize(new
                    {
                        status = report.Status.ToString(),
                        checks = report.Entries.Select(entry => new
                        {
                            name = entry.Key,
                            status = entry.Value.Status.ToString(),
                            description = entry.Value.Description
                        })
                    });
                    await context.Response.WriteAsync(result);
                }
            });
            
            app.UseMvc();
            app.UseHttpsRedirection();
            app.UseCors(option => option.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.UseSwagger();

            app.UseSwaggerUI(x =>
            {
                x.RoutePrefix = string.Empty;
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
        }
    }
}
