using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using PosTech.TechChallenge.Contacts.Command.Infra.Context;

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove the existing DbContext registration
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ContactDbContext>));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            var testDbContextFactory = new TestDbContextFactory();

            services.AddDbContext<ContactDbContext>(options =>
            {
                options.UseSqlServer(testDbContextFactory.ConnectionString);
            });
        });
    }
}
