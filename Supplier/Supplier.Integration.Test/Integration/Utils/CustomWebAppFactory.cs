using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Supplier.Infra.Context;
using System.Linq;

namespace Supplier.Tests.Integration.Utils
{
    public class CustomWebAppFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        public MainContext Db { get; private set; }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var serviceDescription = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<MainContext>));

                if (serviceDescription != null)
                {
                    services.Remove(serviceDescription);
                }

                services.AddDbContext<MainContext>(opt => { opt.UseInMemoryDatabase("SupplierConnectionString"); });

                var sp = services.BuildServiceProvider();
                var scope = sp.CreateScope();

                var scopedServices = scope.ServiceProvider;
                Db = scopedServices.GetRequiredService<MainContext>();

                Db.Database.EnsureCreated();

                services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
            });
        }
    }
}
