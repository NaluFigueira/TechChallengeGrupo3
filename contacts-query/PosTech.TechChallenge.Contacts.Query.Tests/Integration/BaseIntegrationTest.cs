using Bogus;
using System.Net.Http.Headers;
using System.Security.Claims;

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

using PosTech.TechChallenge.Contacts.Query.Api;
using PosTech.TechChallenge.Contacts.Query.Infra.Context;
using PosTech.TechChallenge.Contacts.Query.Infra.Interfaces;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace PosTech.TechChallenge.Contacts.Query.Tests.Integration;

public class BaseIntegrationTest : IClassFixture<WebApplicationFactory<Startup>>
{
    protected readonly HttpClient _httpClient;
    protected readonly ContactDbContext _dbContext;
    protected readonly IContactRepository _contactRepository;

    public BaseIntegrationTest(WebApplicationFactory<Startup> factory)
    {
        _httpClient = factory.CreateDefaultClient();
        var scope = factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();

        _dbContext = scope.ServiceProvider.GetRequiredService<ContactDbContext>();
        _contactRepository = scope.ServiceProvider.GetService<IContactRepository>();
    }

    public HttpClient GetHttpClient()
    {
        return _httpClient;
    }

    public ContactDbContext GetContactDbContext()
    {
        return _dbContext;
    }
    public IContactRepository GetContactRepository()
    {
        return _contactRepository;
    }

    public async Task SetUserTokenInHeaders()
    {
        var token = GenerateToken();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Trim('"'));
    }

    private string GenerateToken()
    {
        var Faker = new Faker("pt_BR");

        Claim[] claims =
        [
            new Claim("username", Faker.Name.FullName()),
            new Claim("id", Guid.NewGuid().ToString()),
            new Claim("loginTimestamp", DateTime.UtcNow.ToString())
        ];

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("c2d4a61141f0616bef9eac3c6cd539c454509dddfed9d0df54a6a17bfbe9172b"));

        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            expires: DateTime.Now.AddMinutes(10),
            claims: claims,
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
