using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;

using PosTech.TechChallenge.Users.Api;
using PosTech.TechChallenge.Users.Api.Configuration;
using PosTech.TechChallenge.Users.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddOpenTelemetry()
    .WithMetrics(metrics => metrics
        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("Contacts.Api"))
        .AddProcessInstrumentation()
        .AddRuntimeInstrumentation()
        .AddHttpClientInstrumentation()
        .AddAspNetCoreInstrumentation()
        .AddPrometheusExporter()
    );

var configurationBuilder = new ConfigurationBuilder();
#if DEBUG
Console.WriteLine("Mode=Debug");
configurationBuilder
    .AddJsonFile("appsettings.Development.json");
#else
Console.WriteLine("Mode=Release");
configurationBuilder
    .AddJsonFile("appsettings.json");
#endif

var startup = new Startup(configurationBuilder.Build());
startup.ConfigureServices(builder.Services);

var app = builder.Build();
startup.Configure(app);
app.UseOpenTelemetryPrometheusScrapingEndpoint();
app.ApplyMigrations();
app.MapUserEndpoints();
app.MapAuthenticationEndpoints();

app.Run();