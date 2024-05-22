using Microsoft.EntityFrameworkCore;

using PosTech.TechChallenge.Contacts.Api;
using PosTech.TechChallenge.Contacts.Infra.Context;

var builder = WebApplication.CreateBuilder(args);
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.AddLogging();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SchemaFilter<DescriptionSchemaFilter>();
});
builder.Services.AddControllers();
builder.Services.AddContactUseCases();
builder.Services.AddDbContext<AplicationDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")),
    ServiceLifetime.Scoped
);

var app = builder.Build();

Console.WriteLine("Applying migrations");

using (var serviceScope = app.Services.CreateScope())
{
    var serviceDb = serviceScope.ServiceProvider
                     .GetService<AplicationDbContext>();

    serviceDb!.Database.Migrate();
}

Console.WriteLine("Done");

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapContactEndpoints();

app.Run();
