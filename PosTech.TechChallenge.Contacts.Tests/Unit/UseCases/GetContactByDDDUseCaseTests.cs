using System.Collections.ObjectModel;

using Moq;

using PosTech.TechChallenge.Contacts.Application;
using PosTech.TechChallenge.Contacts.Domain;
using PosTech.TechChallenge.Contacts.Infra;

namespace PosTech.TechChallenge.Contacts.Tests;

public class GetContactByDDDUseCaseTests
{
    [Fact]
    public async Task ExecuteAsync_ShouldReturnResultOkWithCollectionWithoutContacts_WhenRequestedByDDD()
    {
        // Arrange
        var mockRepository = new Mock<IContactRepository>();

        var selectedDDD = DDDBrazil.DDD_32;
        Collection<Contact> contacts = [];
        var requestDto = new GetContactByDddDTO(selectedDDD);

        mockRepository
            .Setup(repo => repo.GetContactsByDDDAsync(It.IsAny<DDDBrazil>()))
            .ReturnsAsync(contacts);

        var useCase = new GetContactByDDDUseCase(mockRepository.Object);

        // Act
        var result = await useCase.ExecuteAsync(requestDto);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.ValueOrDefault);
        Assert.True(result.IsSuccess);
        Assert.Empty(result.Value);

        mockRepository.Verify(repo => repo.GetContactsByDDDAsync(selectedDDD), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnResultOkWithCollectionWithOneContact_WhenRequestedByDDD()
    {
        // Arrange
        var mockRepository = new Mock<IContactRepository>();

        var selectedDDD = DDDBrazil.DDD_55;
        var contact = new ContactBuilder().WithDDD(selectedDDD).Build();
        Collection<Contact> contacts = [contact];
        var requestDto = new GetContactByDddDTO(selectedDDD);

        mockRepository
            .Setup(repo => repo.GetContactsByDDDAsync(It.IsAny<DDDBrazil>()))
            .ReturnsAsync(contacts);

        var useCase = new GetContactByDDDUseCase(mockRepository.Object);

        // Act
        var result = await useCase.ExecuteAsync(requestDto);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.ValueOrDefault);
        Assert.True(result.IsSuccess);
        Assert.Single(result.Value);

        Assert.Equal(contact.DDD, result.Value.ElementAt(0).DDD);
        Assert.Equal(contact.Name, result.Value.ElementAt(0).Name);
        Assert.Equal(contact.PhoneNumber, result.Value.ElementAt(0).PhoneNumber);
        Assert.Equal(contact.Email, result.Value.ElementAt(0).Email);

        mockRepository.Verify(repo => repo.GetContactsByDDDAsync(selectedDDD), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnResultOkWithCollectionWithTwoContacts_WhenRequestedByDDD()
    {
        // Arrange
        var mockRepository = new Mock<IContactRepository>();

        var selectedDDD = DDDBrazil.DDD_21;
        var contact1 = new ContactBuilder().WithDDD(selectedDDD).Build();
        var contact2 = new ContactBuilder().WithDDD(selectedDDD).Build();
        var contact3 = new ContactBuilder().WithDDD(selectedDDD).Build();
        Collection<Contact> contacts = [
            contact1,
            contact2,
            contact3
        ];
        var requestDto = new GetContactByDddDTO(selectedDDD);

        mockRepository
            .Setup(repo => repo.GetContactsByDDDAsync(It.IsAny<DDDBrazil>()))
            .ReturnsAsync(contacts);

        var useCase = new GetContactByDDDUseCase(mockRepository.Object);

        // Act
        var result = await useCase.ExecuteAsync(requestDto);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.ValueOrDefault);
        Assert.True(result.IsSuccess);
        Assert.NotEmpty(result.Value);
        Assert.Equal(contacts.Count, result.Value.Count);

        for (int i = 0; i < contacts.Count; i++)
        {
            Assert.Equal(contacts[i].DDD, result.Value.ElementAt(i).DDD);
            Assert.Equal(contacts[i].Name, result.Value.ElementAt(i).Name);
            Assert.Equal(contacts[i].PhoneNumber, result.Value.ElementAt(i).PhoneNumber);
            Assert.Equal(contacts[i].Email, result.Value.ElementAt(i).Email);
        }

        mockRepository.Verify(repo => repo.GetContactsByDDDAsync(selectedDDD), Times.Once);
    }

}
