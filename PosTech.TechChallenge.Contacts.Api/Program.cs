using Microsoft.EntityFrameworkCore;

using PosTech.TechChallenge.Contacts.Api;
using PosTech.TechChallenge.Contacts.Infra.Context;

var builder = WebApplication.CreateBuilder(args);
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddContactUseCases();
builder.Services.AddDbContext<AplicationDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")),
    ServiceLifetime.Scoped
);

var app = builder.Build();

Console.WriteLine("Applying migrations");

using (var context = (AplicationDbContext)app.Services.GetService(typeof(AplicationDbContext)))
{
    context.Database.Migrate();
}

Console.WriteLine("Done");

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapContactEndpoints();

app.Run();
