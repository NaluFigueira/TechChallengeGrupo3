using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

using PosTech.TechChallenge.Contacts.Api;
using PosTech.TechChallenge.Contacts.Infra.Context;

namespace PosTech.TechChallenge.Contacts.Tests;

public class BaseIntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
{
    protected readonly HttpClient _httpClient;
    protected readonly AplicationDbContext _dbContext;

    public BaseIntegrationTests(WebApplicationFactory<Startup> factory)
    {
        _httpClient = factory.CreateDefaultClient();
        var scope = factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        _dbContext = scope.ServiceProvider.GetRequiredService<AplicationDbContext>();
    }
}
