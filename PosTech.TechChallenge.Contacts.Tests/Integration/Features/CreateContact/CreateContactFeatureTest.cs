using System.Net;
using System.Net.Http.Json;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc.Testing;

using PosTech.TechChallenge.Contacts.Api;
using PosTech.TechChallenge.Contacts.Domain;

using Xunit.Gherkin.Quick;

namespace PosTech.TechChallenge.Contacts.Tests.Integration;

[FeatureFile("Integration/Features/CreateContact/CreateContact.feature")]
public sealed class CreateContactFeatureTest : Feature
{
    private readonly BaseIntegrationTests _base;
    private Contact? _contactData;
    private HttpResponseMessage? _result;

    public CreateContactFeatureTest()
    {
        var factory = new WebApplicationFactory<Startup>();
        _base = new BaseIntegrationTests(factory);
    }


    [Given(@"a user who has access to the creation endpoint")]
    public async Task GivenAUserWhoHasAccessToTheCreationEndpoint()
    {
        await _base.SetUserTokenInHeaders();
    }

    [When(@"they fill in valid contact information")]
    public void WhenTheyFillInValidContactInformation()
    {
        _contactData = new ContactBuilder().Build();
    }

    [And(@"they send the inputted contact information through the endpoint")]
    public async Task AndTheySendTheInputtedContactInformationThroughTheEndpoint()
    {
        _result = await _base.GetHttpClient().PostAsJsonAsync("/contacts", _contactData);

    }

    [Then(@"the API should add new contact to list")]
    public async Task ThenTheAPIShouldAddNewContactToList()
    {
        await _base.ClearUser();
        var dbContext = _base.GetAplicationDbContext();
        var createdContact = dbContext.Contact.FirstOrDefault(c => c.Name == _contactData.Name);
        dbContext.Contact.Remove(createdContact!);
        await dbContext.SaveChangesAsync();

        _result?.StatusCode.Should().Be(HttpStatusCode.Created);
        createdContact.Should().NotBeNull();
        createdContact?.PhoneNumber.Should().Be(createdContact.PhoneNumber);
        createdContact?.Email.Should().Be(createdContact.Email);
        createdContact?.DDD.Should().Be(createdContact.DDD);
    }
}
