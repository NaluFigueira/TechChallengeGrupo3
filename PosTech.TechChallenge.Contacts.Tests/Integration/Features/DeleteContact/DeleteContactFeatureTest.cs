using System.Net;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc.Testing;

using PosTech.TechChallenge.Contacts.Api;

using Xunit.Gherkin.Quick;

namespace PosTech.TechChallenge.Contacts.Tests.Integration;

[FeatureFile("Integration/Features/DeleteContact/DeleteContact.feature")]
public sealed class DeleteContactFeatureTest : Feature
{
    private readonly BaseIntegrationTests _base;
    private Guid _contactId = Guid.NewGuid();
    private HttpResponseMessage? _result;

    public DeleteContactFeatureTest()
    {
        var factory = new WebApplicationFactory<Startup>();
        _base = new BaseIntegrationTests(factory);
    }


    [Given(@"a user who has access to the deletion endpoint")]
    public async Task GivenAUserWhoHasAccessToTheCreationEndpoint()
    {
        await _base.SetUserTokenInHeaders();
    }

    [When(@"they send the desired contact id through the endpoint")]
    public async Task WhenTheySendTheDesiredContactIdThroughTheEndpoint()
    {
        var contact = new ContactBuilder().Build();
        var dbContext = _base.GetAplicationDbContext();
        dbContext.Contact.Add(contact);
        await dbContext.SaveChangesAsync();
        _contactId = contact.Id;

        _result = await _base.GetHttpClient().DeleteAsync($"/contacts/{contact.Id}");
    }

    [Then(@"the API should remove the contact from the list")]
    public async Task ThenTheAPIShouldRemoveContactFromTheList()
    {
        await _base.ClearUser();
        _result.StatusCode.Should().Be(HttpStatusCode.NoContent);
        var foundContact = await _base.GetContactRepository().GetContactByIdAsync(_contactId);
        foundContact.Should().BeNull();
    }
}
