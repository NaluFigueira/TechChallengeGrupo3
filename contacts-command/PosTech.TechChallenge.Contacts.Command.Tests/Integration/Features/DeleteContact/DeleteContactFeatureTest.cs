﻿using System.Net;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc.Testing;

using PosTech.TechChallenge.Contacts.Command.Api;


namespace PosTech.TechChallenge.Contacts.Tests.Integration;

public sealed class DeleteContactFeatureTest(CustomWebApplicationFactory<Startup> factory) : BaseIntegrationTests(factory), IDisposable
{
    private Guid _contactId = Guid.NewGuid();
    private HttpResponseMessage? _result;

    [Fact]
    public async Task Test()
    {
        SetUserTokenInHeaders();
        var contact = new ContactBuilder().Build();
        var dbContext = GetContactDbContext();
        dbContext.Contact.Add(contact);
        await dbContext.SaveChangesAsync();
        _contactId = contact.Id;

        _result = await GetHttpClient().DeleteAsync($"/contacts/{contact.Id}");
        _result.StatusCode.Should().Be(HttpStatusCode.NoContent);
        var foundContact = await GetContactRepository().GetContactByIdAsync(_contactId);
        foundContact.Should().BeNull();
    }

    public void Dispose()
    {
        var dbContext = GetContactDbContext();
        dbContext.Database.EnsureDeleted(); // Clean up the database after each test
        dbContext.Dispose();
    }
}
