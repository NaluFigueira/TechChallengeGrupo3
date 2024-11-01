using System.Net;
using System.Net.Http.Json;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc.Testing;

using PosTech.TechChallenge.Contacts.Command.Api;
using PosTech.TechChallenge.Contacts.Command.Domain;



namespace PosTech.TechChallenge.Contacts.Tests.Integration;


public sealed class CreateContactFeatureTest(WebApplicationFactory<Startup> factory) : BaseIntegrationTests(factory)
{
    private Contact? _contactData;
    private HttpResponseMessage? _result;

    [Fact]
    public async Task Test()
    {
        SetUserTokenInHeaders();
        _contactData = new ContactBuilder().Build();
        _result = await GetHttpClient().PostAsJsonAsync("/contacts", _contactData);
        var dbContext = GetContactDbContext();
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
