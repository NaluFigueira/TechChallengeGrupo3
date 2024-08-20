using System.Net.Http.Headers;

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

using PosTech.TechChallenge.Contacts.Command.Api;
using PosTech.TechChallenge.Contacts.Command.Application;
using PosTech.TechChallenge.Contacts.Command.Infra;
using PosTech.TechChallenge.Contacts.Command.Infra.Context;

namespace PosTech.TechChallenge.Contacts.Tests;

public class BaseIntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
{
    protected readonly HttpClient _httpClient;
    protected readonly AplicationDbContext _dbContext;
    protected readonly UserDbContext _userContext;
    protected readonly IContactRepository _contactRepository;
    private CreateUserDTO _userDTO;

    public BaseIntegrationTests(WebApplicationFactory<Startup> factory)
    {
        _httpClient = factory.CreateDefaultClient();
        var scope = factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();

        _dbContext = scope.ServiceProvider.GetRequiredService<AplicationDbContext>();
        _userContext = scope.ServiceProvider.GetRequiredService<UserDbContext>();
        _contactRepository = scope.ServiceProvider.GetService<IContactRepository>();
    }

    public HttpClient GetHttpClient()
    {
        return _httpClient;
    }

    public AplicationDbContext GetAplicationDbContext()
    {
        return _dbContext;
    }

    public UserDbContext GetUserDbContext()
    {
        return _userContext;
    }

    public IContactRepository GetContactRepository()
    {
        return _contactRepository;
    }

    public async Task SetUserTokenInHeaders()
    {
        _userDTO = new CreateUserDTOBuilder()
                        .WithPassword("S3cur3P@ssW0rd")
                        .WithRePassword("S3cur3P@ssW0rd")
                        .Build();
        await _httpClient.PostAsJsonAsync("/users", _userDTO);

        var loginDTO = new LoginDTO()
        {
            UserName = _userDTO.UserName,
            Password = _userDTO.Password,
        };

        var result = await _httpClient.PostAsJsonAsync("/login", loginDTO);
        var token = await result.Content.ReadAsStringAsync();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Trim('"'));
    }

    public async Task ClearUser()
    {
        var createdUser = _userContext.Users.FirstOrDefault(c => c.UserName == _userDTO.UserName);
        if (createdUser is not null)
        {
            _userContext.Users.Remove(createdUser!);
            await _userContext.SaveChangesAsync();
        }
    }
}
