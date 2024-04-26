using System.Collections.ObjectModel;

using Moq;

using PosTech.TechChallenge.Contacts.Application.Repositories;
using PosTech.TechChallenge.Contacts.Application.UnitTests.Utilities;
using PosTech.TechChallenge.Contacts.Application.UseCases.GetContactsByDDDUseCase;
using PosTech.TechChallenge.Contacts.Domain.Entities;
using PosTech.TechChallenge.Contacts.Domain.Enums;

namespace PosTech.TechChallenge.Contacts.Application.UnitTests;

public class GetContactsByDDDUseCaseTests
{
    [Fact]
    public async Task ExecuteAsync_ShouldReturnResultOkWithCollectionWithoutContacts_WhenRequestedByDDD()
    {
        // Arrange
        var mockRepository = new Mock<IContactRepository>();

        var selectedDDD = DDDBrazil.DDD_32;
        Collection<Contact> contacts = [];
        var requestDto = new GetContactsByDDDRequestDto(DDD: selectedDDD);

        mockRepository
            .Setup(repo => repo.GetContactsByDDDAsync(It.IsAny<DDDBrazil>()))
            .ReturnsAsync(contacts);

        var useCase = new GetContactsByDDDUseCase(mockRepository.Object);

        // Act
        var result = await useCase.ExecuteAsync(requestDto);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.ValueOrDefault);
        Assert.True(result.IsSuccess);
        Assert.Empty(result.Value.Contacts);

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
        var requestDto = new GetContactsByDDDRequestDto(DDD: selectedDDD);

        mockRepository
            .Setup(repo => repo.GetContactsByDDDAsync(It.IsAny<DDDBrazil>()))
            .ReturnsAsync(contacts);

        var useCase = new GetContactsByDDDUseCase(mockRepository.Object);

        // Act
        var result = await useCase.ExecuteAsync(requestDto);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.ValueOrDefault);
        Assert.True(result.IsSuccess);
        Assert.Single(result.Value.Contacts);

        Assert.Equal(contact.DDD, result.Value.Contacts[0].DDD);
        Assert.Equal(contact.Name, result.Value.Contacts[0].Name);
        Assert.Equal(contact.PhoneNumber, result.Value.Contacts[0].PhoneNumber);
        Assert.Equal(contact.Email, result.Value.Contacts[0].Email);

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
        var requestDto = new GetContactsByDDDRequestDto(DDD: selectedDDD);

        mockRepository
            .Setup(repo => repo.GetContactsByDDDAsync(It.IsAny<DDDBrazil>()))
            .ReturnsAsync(contacts);

        var useCase = new GetContactsByDDDUseCase(mockRepository.Object);

        // Act
        var result = await useCase.ExecuteAsync(requestDto);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.ValueOrDefault);
        Assert.True(result.IsSuccess);
        Assert.NotEmpty(result.Value.Contacts);
        Assert.Equal(contacts.Count, result.Value.Contacts.Count);

        for (int i = 0; i < contacts.Count; i++)
        {
            Assert.Equal(contacts[i].DDD, result.Value.Contacts[i].DDD);
            Assert.Equal(contacts[i].Name, result.Value.Contacts[i].Name);
            Assert.Equal(contacts[i].PhoneNumber, result.Value.Contacts[i].PhoneNumber);
            Assert.Equal(contacts[i].Email, result.Value.Contacts[i].Email);
        }

        mockRepository.Verify(repo => repo.GetContactsByDDDAsync(selectedDDD), Times.Once);
    }
}
