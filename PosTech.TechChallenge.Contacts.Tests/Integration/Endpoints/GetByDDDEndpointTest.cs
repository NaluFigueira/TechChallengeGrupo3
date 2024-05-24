using System.Net.Http;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc.Testing;

using PosTech.TechChallenge.Contacts.Api;
using PosTech.TechChallenge.Contacts.Domain;

namespace PosTech.TechChallenge.Contacts.Tests;

[Trait("Integration Tests", nameof(GetByDDDEndpointTest))]
public class GetByDDDEndpointTest(WebApplicationFactory<Startup> factory) : BaseIntegrationTests(factory)
{
    [Fact]
    public async Task ShouldBeAbleToGetContactsByDDD()
    {

        //Arrange
        var contact1 = new ContactBuilder().WithDDD(DDDBrazil.DDD_11).Build();
        var contact2 = new ContactBuilder().WithDDD(DDDBrazil.DDD_18).Build();
        _dbContext.Contact.AddRange([contact1, contact2]);
        await _dbContext.SaveChangesAsync();

        //Act
        var result = await _httpClient.GetAsync($"/contacts?ddd={contact1.DDD}");
        var obtainedContacts = await result.Content.ReadAsAsync<Contact[]>();

        //Assert
        _dbContext.Contact.Remove(contact1);
        _dbContext.Contact.Remove(contact2);
        await _dbContext.SaveChangesAsync();

        result.Should().BeSuccessful();
        obtainedContacts.Should().Contain(c => c.Id == contact1.Id);
        obtainedContacts.Should().NotContain(c => c.Id == contact2.Id);
    }
}
