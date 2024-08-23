using System.Net;
using System.Net.Http.Json;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc.Testing;

using PosTech.TechChallenge.Users.Api;
using PosTech.TechChallenge.Users.Application.DTOs;
using PosTech.TechChallenge.Users.Tests.Builders;

using Xunit.Gherkin.Quick;

namespace PosTech.TechChallenge.Users.Tests.Integration;

[FeatureFile("Integration/Features/AuthenticateUser/AuthenticateUser.feature")]
public class AuthenticateUserFeatureTest : Feature
{
    private readonly BaseIntegrationTests _base;
    private LoginDTO _loginDTO;

    public AuthenticateUserFeatureTest()
    {
        var factory = new WebApplicationFactory<Startup>();
        _base = new BaseIntegrationTests(factory);
    }


    [Given(@"an existing user who has access to the log in endpoint")]
    public static void GivenAnExistingUserWhoHasAccessToTheLogInEndpoint()
    {
    }

    [When(@"they fill in a username ""(\w+)"" and password ""(.+)""")]
    public async Task WhenTheyFillInAnUserNameAndPassword(string username, string password)
    {
        var userDto = new CreateUserDTOBuilder()
                            .WithUserName(username)
                            .WithPassword(password)
                            .WithRePassword(password)
                            .Build();
        await _base.GetHttpClient().PostAsJsonAsync("/users", userDto);


        _loginDTO = new LoginDTO()
        {
            UserName = username,
            Password = password,
        };
    }

    [Then(@"the API should authenticate correctly")]
    public async Task ThenTheApiShouldAuthenticateCorrectly()
    {
        var result = await _base.GetHttpClient().PostAsJsonAsync("/login", _loginDTO);

        var dbContext = _base.GetUserDbContext();
        var createdUser = dbContext.Users.FirstOrDefault(c => c.UserName == _loginDTO.UserName);
        if (createdUser is not null)
        {
            dbContext.Users.Remove(createdUser!);
            await dbContext.SaveChangesAsync();
        }

        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        var obtainedToken = await result.Content.ReadAsStringAsync();
        obtainedToken.Should().NotBeNullOrEmpty();
    }
}

