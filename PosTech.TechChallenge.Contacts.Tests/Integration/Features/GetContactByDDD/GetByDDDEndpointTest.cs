using System.Net.Http;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc.Testing;

using PosTech.TechChallenge.Contacts.Api;
using PosTech.TechChallenge.Contacts.Domain;

using Xunit.Gherkin.Quick;

namespace PosTech.TechChallenge.Contacts.Tests;

[FeatureFile("Integration/Features/GetContactByDDD/GetContactByDDD.feature")]
public sealed class GetContactByDDDFeatureTest : Feature
{
    private readonly BaseIntegrationTests _base;
    private Contact[]? _contactData;
    private HttpResponseMessage? _result;

    public GetContactByDDDFeatureTest()
    {
        var factory = new WebApplicationFactory<Startup>();
        _base = new BaseIntegrationTests(factory);
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
        var dbContext = _base.GetAplicationDbContext();
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
        await _base.ClearUser();
        var dbContext = _base.GetAplicationDbContext();
        dbContext.RemoveRange(_contactData);
        await dbContext.SaveChangesAsync();

        _result.Should().BeSuccessful();
        var obtainedContacts = await _result.Content.ReadAsAsync<Contact[]>();
        obtainedContacts.Should().OnlyContain(c => c.DDD == DDDBrazil.DDD_11);
    }
}