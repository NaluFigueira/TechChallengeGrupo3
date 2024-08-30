using FluentAssertions;

using Microsoft.AspNetCore.Mvc.Testing;

using PosTech.TechChallenge.Contacts.Query.Api;
using PosTech.TechChallenge.Contacts.Query.Domain.Entities;
using PosTech.TechChallenge.Contacts.Query.Domain.Enum;
using PosTech.TechChallenge.Contacts.Query.Tests.Builders;

using Xunit.Gherkin.Quick;

namespace PosTech.TechChallenge.Contacts.Query.Tests.Integration.Features.GetContactByDDD;

[FeatureFile("Integration/Features/GetContactByDDD/GetContactByDDD.feature")]
public sealed class GetContactByDDDFeatureTest : Feature
{
    private readonly BaseIntegrationTest _base;
    private Contact[]? _contactData;
    private HttpResponseMessage? _result;

    public GetContactByDDDFeatureTest()
    {
        var factory = new WebApplicationFactory<Startup>();
        _base = new BaseIntegrationTest(factory);
    }


    [Given(@"a user who has access to the get by ddd endpoint")]
    public async Task GivenAUserWhoHasAccessToTheGetByDDDEndpoint()
    {
        await _base.SetUserTokenInHeaders();
    }

    [And(@"user has two contacts with ddd 11 and one with ddd 18")]
    public async Task AndUserHasTwoContactsWithDdd11AndOneWithDdd18()
    {
        var contact1 = new ContactBuilder().WithDDD(DDDBrazil.DDD_11).Build();
        var contact2 = new ContactBuilder().WithDDD(DDDBrazil.DDD_11).Build();
        var contact3 = new ContactBuilder().WithDDD(DDDBrazil.DDD_18).Build();
        _contactData = [contact1, contact2, contact3];
        var dbContext = _base.GetContactDbContext();
        dbContext.Contact.AddRange(_contactData);
        await dbContext.SaveChangesAsync();
    }

    [When(@"they search a contact with ddd 11")]
    public async Task WhenTheySearchAContactWithDdd11()
    {
        _result = await _base.GetHttpClient().GetAsync($"/contacts?ddd={DDDBrazil.DDD_11}");
    }

    [Then(@"the API should return only contacts with ddd 11")]
    public async Task ThenTheAPIShouldReturnOnlyContactsWithDdd11()
    {
        var dbContext = _base.GetContactDbContext();
        dbContext.RemoveRange(_contactData);
        await dbContext.SaveChangesAsync();

        _result.Should().BeSuccessful();
        var obtainedContacts = await _result.Content.ReadAsAsync<Contact[]>();
        obtainedContacts.Should().OnlyContain(c => c.DDD == DDDBrazil.DDD_11);
    }
}