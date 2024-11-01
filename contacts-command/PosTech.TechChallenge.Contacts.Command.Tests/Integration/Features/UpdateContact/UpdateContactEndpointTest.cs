using System.Net;
using System.Net.Http.Json;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc.Testing;

using PosTech.TechChallenge.Contacts.Command.Api;
using PosTech.TechChallenge.Contacts.Command.Application;
using PosTech.TechChallenge.Contacts.Command.Domain;


namespace PosTech.TechChallenge.Contacts.Tests.Integration;


public sealed class UpdateContactFeatureTest(CustomWebApplicationFactory<Startup> factory) : BaseIntegrationTests(factory), IDisposable
{
    private Contact _contact;
    private HttpResponseMessage? _result;

    [Fact]
    public async Task Test()
    {
        SetUserTokenInHeaders();
        var contact = new ContactBuilder().Build();
        var dbContext = GetContactDbContext();
        dbContext.Contact.Add(contact);
        await dbContext.SaveChangesAsync();
        _contact = contact;

        _result = await GetHttpClient().PatchAsJsonAsync("/contacts",
                                                        new UpdateContactDTO(
                                                            Id: contact.Id,
                                                            DDD: contact.DDD,
                                                            Email: contact.Email,
                                                            Name: "NewName",
                                                            PhoneNumber: contact.PhoneNumber
                                                        ));
        var updatedContact = await GetContactRepository().GetContactByIdAsync(_contact.Id);
        dbContext.Contact.Remove(_contact);
        await dbContext.SaveChangesAsync();

        _result.StatusCode.Should().Be(HttpStatusCode.NoContent);
        updatedContact.Should().NotBeNull();
        updatedContact?.Name.Should().Be("NewName");
        updatedContact?.Email.Should().Be(_contact.Email);
        updatedContact?.DDD.Should().Be(_contact.DDD);
        updatedContact?.PhoneNumber.Should().Be(_contact.PhoneNumber);
    }

    public void Dispose()
    {
        var dbContext = GetContactDbContext();
        dbContext.Database.EnsureDeleted(); // Clean up the database after each test
        dbContext.Dispose();
    }
}