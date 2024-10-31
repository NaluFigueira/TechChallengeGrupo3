using Microsoft.EntityFrameworkCore;

using PosTech.TechChallenge.Contacts.Command.Infra;
using PosTech.TechChallenge.Contacts.Command.Infra.Context;

namespace PosTech.TechChallenge.Contacts.Command.Api.Configuration;

public static class MigrationInitializer
{
    public static void ApplyMigrations(this WebApplication app)
    {
#if RELEASE
        Console.WriteLine("Applying migrations");
        using (var serviceScope = app.Services.CreateScope())
        {
            Console.WriteLine("Contact Command DB Migrations...");
            var contactServiceDb = serviceScope.ServiceProvider
                             .GetService<ContactDbContext>();
            var configurationBuilder = new ConfigurationBuilder();
            Console.WriteLine("Mode=Debug");
                contactServiceDb!.Database.Migrate();
        }
#else
#endif
        Console.WriteLine("Done");
    }
}