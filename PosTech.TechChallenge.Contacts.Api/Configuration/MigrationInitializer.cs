using Microsoft.EntityFrameworkCore;

using PosTech.TechChallenge.Contacts.Infra.Context;

namespace PosTech.TechChallenge.Contacts.Api.Configuration;

public static class MigrationInitializer
{
    public static void ApplyMigrations(this WebApplication app)
    {
        Console.WriteLine("Applying migrations");
        using (var serviceScope = app.Services.CreateScope())
        {
            var serviceDb = serviceScope.ServiceProvider
                             .GetService<AplicationDbContext>();
            serviceDb!.Database.Migrate();
        }
        Console.WriteLine("Done");
    }
}
