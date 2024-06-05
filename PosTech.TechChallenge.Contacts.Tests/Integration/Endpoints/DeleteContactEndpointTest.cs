using System.Net;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc.Testing;

using PosTech.TechChallenge.Contacts.Api;

namespace PosTech.TechChallenge.Contacts.Tests;

[Trait("Integration Tests", nameof(DeleteContactEndpointTest))]
public class DeleteContactEndpointTest(WebApplicationFactory<Startup> factory) : BaseIntegrationTests(factory)
{
    [Fact]
    public async Task ShouldBeAbleToDeleteContact()
    {

        //Arrange
        var contact = new ContactBuilder().Build();
        _dbContext.Contact.Add(contact);
        await _dbContext.SaveChangesAsync();

        //Act
        var result = await _httpClient.DeleteAsync($"/contacts/{contact.Id}");

        //Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
        var foundContact = await _contactRepository.GetContactByIdAsync(contact.Id);
        foundContact.Should().BeNull();
    }
}
