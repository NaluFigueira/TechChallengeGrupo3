using System.Net;
using System.Net.Http.Json;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc.Testing;

using PosTech.TechChallenge.Contacts.Api;

namespace PosTech.TechChallenge.Contacts.Tests;

[Trait("Integration Tests", nameof(CreateContactEndpointTest))]
public class CreateContactEndpointTest(WebApplicationFactory<Startup> factory) : BaseIntegrationTests(factory)
{
    [Fact]
    public async Task ShouldBeAbleToCreateContact()
    {

        //Arrange
        var contact = new ContactBuilder().Build();

        //Act
        var result = await _httpClient.PostAsJsonAsync("/contacts", contact);

        //Assert
        var createdContact = _dbContext.Contact.FirstOrDefault(c => c.Name == contact.Name);
        _dbContext.Contact.Remove(createdContact!);
        await _dbContext.SaveChangesAsync();
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}
