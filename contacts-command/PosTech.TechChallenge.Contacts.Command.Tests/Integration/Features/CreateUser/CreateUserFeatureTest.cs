using System.Net;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc.Testing;

using PosTech.TechChallenge.Contacts.Command.Api;

using Xunit.Gherkin.Quick;

namespace PosTech.TechChallenge.Contacts.Tests.Integration;

[FeatureFile("Integration/Features/CreateUser/CreateUser.feature")]
public class CreateUserFeatureTest : Feature
{
    private readonly BaseIntegrationTests _base;
    private CreateUserDTOBuilder _builder;

    public CreateUserFeatureTest()
    {
        var factory = new WebApplicationFactory<Startup>();
        _base = new BaseIntegrationTests(factory);
    }


    [Given(@"a potential user who has access to the creation endpoint")]
    public static void GivenAPotentialUserWhoHasAccessToTheCreationEndpoint()
    {
    }

    [When(@"they fill in a username ""(\w+)"", an e-mail ""(.+)"", a valid password ""(.+)""")]
    public void WhenTheyFillInANameAnEmailAValidPassword(string username, string email, string password)
    {
        _builder = new CreateUserDTOBuilder().WithUserName(username).WithEmail(email).WithPassword(password);
    }

    [And(@"they confirm the password with ""(.+)""")]
    public void AndTheyConfirmThePassword(string repassword)
    {
        _builder = _builder.WithRePassword(repassword);
    }

    [Then(@"the API should add the new user to its database")]
    public async Task ThenTheApiShouldAddTheNewUserToItsDatabase()
    {
        var userDto = _builder.Build();
        var result = await _base.GetHttpClient().PostAsJsonAsync("/users", userDto);

        var dbContext = _base.GetUserDbContext();
        var createdUser = dbContext.Users.FirstOrDefault(c => c.UserName == userDto.UserName);
        if (createdUser is not null)
        {
            dbContext.Users.Remove(createdUser!);
            await dbContext.SaveChangesAsync();
        }

        result?.StatusCode.Should().Be(HttpStatusCode.Created);
        createdUser.Should().NotBeNull();
        createdUser?.Email.Should().Be(userDto.Email);
    }
}
