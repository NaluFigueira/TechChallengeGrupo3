using System.Net;
using System.Net.Http.Json;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc.Testing;

using PosTech.TechChallenge.Contacts.Api;
using PosTech.TechChallenge.Contacts.Application;

namespace PosTech.TechChallenge.Contacts.Tests;

[Trait("Integration Tests", nameof(UpdateContactEndpointTest))]
public class UpdateContactEndpointTest(WebApplicationFactory<Startup> factory) : BaseIntegrationTests(factory)
{
    [Fact]
    public async Task ShouldBeAbleToUpdateContact()
    {

        //Arrange
        var contact = new ContactBuilder().Build();
        _dbContext.Contact.Add(contact);
        await _dbContext.SaveChangesAsync();

        //Act
        var result = await _httpClient.PatchAsJsonAsync("/contacts",
                                                        new UpdateContactDTO(
                                                            Id: contact.Id,
                                                            DDD: contact.DDD,
                                                            Email: contact.Email,
                                                            Name: "NewName",
                                                            PhoneNumber: contact.PhoneNumber
                                                        ));

        //Assert
        _dbContext.Contact.Remove(contact);
        await _dbContext.SaveChangesAsync();
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}
