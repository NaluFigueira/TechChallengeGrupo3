using Microsoft.EntityFrameworkCore;

using PosTech.TechChallenge.Contacts.Query.Infra.Context;

namespace PosTech.TechChallenge.Contacts.Query.Api.Configuration;

public static class MigrationInitializer
{
    public static void ApplyMigrations(this WebApplication app)
    {
        Console.WriteLine("Applying migrations");
        using (var serviceScope = app.Services.CreateScope())
        {
            Console.WriteLine("Contacts...");
            var aplicationServiceDb = serviceScope.ServiceProvider
                             .GetService<ContactDbContext>();
            aplicationServiceDb!.Database.Migrate();
        }
        Console.WriteLine("Done");
    }
}
