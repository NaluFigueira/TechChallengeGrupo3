using System.Net.Http.Headers;
using System.Net.Http.Json;

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

using PosTech.TechChallenge.Users.Api;
using PosTech.TechChallenge.Users.Application.DTOs;
using PosTech.TechChallenge.Users.Infra.Context;
using PosTech.TechChallenge.Users.Tests.Builders;

namespace PosTech.TechChallenge.Users.Tests.Integration;

public class BaseIntegrationTests : IClassFixture<WebApplicationFactory<Startup>>
{
    protected readonly HttpClient _httpClient;
    protected readonly UserDbContext _userContext;
    private CreateUserDTO _userDTO;

    public BaseIntegrationTests(WebApplicationFactory<Startup> factory)
    {
        _httpClient = factory.CreateDefaultClient();
        var scope = factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();

        _userContext = scope.ServiceProvider.GetRequiredService<UserDbContext>();
    }

    public HttpClient GetHttpClient()
    {
        return _httpClient;
    }

    public UserDbContext GetUserDbContext()
    {
        return _userContext;
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

