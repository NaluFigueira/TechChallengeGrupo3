using System.Net;
using System.Net.Http.Json;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc.Testing;

using PosTech.TechChallenge.Contacts.Api;
using PosTech.TechChallenge.Contacts.Application;
using PosTech.TechChallenge.Contacts.Domain;

using Xunit.Gherkin.Quick;

namespace PosTech.TechChallenge.Contacts.Tests;


[FeatureFile("Integration/Features/UpdateContact/UpdateContact.feature")]
public sealed class UpdateContactFeatureTest : Feature
{
    private readonly BaseIntegrationTests _base;
    private Contact _contact;
    private HttpResponseMessage? _result;

    public UpdateContactFeatureTest()
    {
        var factory = new WebApplicationFactory<Startup>();
        _base = new BaseIntegrationTests(factory);
    }


    [Given(@"a user who has access to the update endpoint")]
    public void GivenAUserWhoHasAccessToTheUpdateEndpoint()
    {
    }

    [When(@"they update a contact name to NewName")]
    public async Task WhenTheyUpdateAContactNameToNewName()
    {
        var contact = new ContactBuilder().Build();
        var dbContext = _base.GetAplicationDbContext();
        dbContext.Contact.Add(contact);
        await dbContext.SaveChangesAsync();
        _contact = contact;

        _result = await _base.GetHttpClient().PatchAsJsonAsync("/contacts",
                                                        new UpdateContactDTO(
                                                            Id: contact.Id,
                                                            DDD: contact.DDD,
                                                            Email: contact.Email,
                                                            Name: "NewName",
                                                            PhoneNumber: contact.PhoneNumber
                                                        ));
    }

    [Then(@"the API should update contact correctly")]
    public async Task ThenTheAPIShouldUpdateContactCorrectly()
    {
        var updatedContact = await _base.GetContactRepository().GetContactByIdAsync(_contact.Id);
        var dbContext = _base.GetAplicationDbContext();

        dbContext.Contact.Remove(_contact);
        await dbContext.SaveChangesAsync();

        _result.StatusCode.Should().Be(HttpStatusCode.NoContent);
        updatedContact.Should().NotBeNull();
        updatedContact?.Name.Should().Be("NewName");
        updatedContact?.Email.Should().Be(_contact.Email);
        updatedContact?.DDD.Should().Be(_contact.DDD);
        updatedContact?.PhoneNumber.Should().Be(_contact.PhoneNumber);
    }
}