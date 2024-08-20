using Microsoft.EntityFrameworkCore;

using PosTech.TechChallenge.Contacts.Command.Infra;
using PosTech.TechChallenge.Contacts.Command.Infra.Context;

namespace PosTech.TechChallenge.Contacts.Command.Api.Configuration;

public static class MigrationInitializer
{
    public static void ApplyMigrations(this WebApplication app)
    {
        Console.WriteLine("Applying migrations");
        using (var serviceScope = app.Services.CreateScope())
        {
            Console.WriteLine("Users...");
            var userServiceDb = serviceScope.ServiceProvider
                             .GetService<UserDbContext>();
            userServiceDb!.Database.Migrate();

            Console.WriteLine("Application...");
            var aplicationServiceDb = serviceScope.ServiceProvider
                             .GetService<AplicationDbContext>();
            aplicationServiceDb!.Database.Migrate();
        }
        Console.WriteLine("Done");
    }
}
