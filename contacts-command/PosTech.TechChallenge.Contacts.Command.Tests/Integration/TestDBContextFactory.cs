
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using PosTech.TechChallenge.Contacts.Command.Infra.Context;

public class TestDbContextFactory
{
    public readonly string ConnectionString;

    public TestDbContextFactory()
    {
        // Generate a unique database name for each test run
        var databaseName = $"TechChallenge_Test_{Guid.NewGuid():N}";
        ConnectionString = $"Server=localhost,1435;Database={databaseName};User=sa;Password=S3cur3P@ssW0rd;Trusted_Connection=False; TrustServerCertificate=True; Encrypt=False";
        CreateDbContext();
    }

    public ContactDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<ContactDbContext>()
            .UseSqlServer(ConnectionString)
            .Options;

        var context = new ContactDbContext(options);

        // Ensure the database is created and apply migrations
        if (context.Database is not null)
        {
            context.Database.EnsureDeleted();
        }
        context.Database.EnsureCreated();

        return context;
    }

    public void Dispose()
    {
        // Delete the database after tests are done
        using (var context = CreateDbContext())
        {
            context.Database.EnsureDeleted();
        }
    }
}
