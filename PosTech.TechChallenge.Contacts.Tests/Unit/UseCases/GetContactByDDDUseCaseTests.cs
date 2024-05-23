using System.Collections.ObjectModel;

using FluentAssertions;

using Microsoft.Extensions.Logging;

using Moq;

using PosTech.TechChallenge.Contacts.Application;
using PosTech.TechChallenge.Contacts.Domain;
using PosTech.TechChallenge.Contacts.Infra;

namespace PosTech.TechChallenge.Contacts.Tests;

public class GetContactByDDDUseCaseTests
{
    [Fact]
    public async Task ExecuteAsync_WhenRequestedByDDD_ShouldReturnResultOkWithCollectionWithoutContacts()
    {
        // Arrange
        var mockRepository = new Mock<IContactRepository>();
        var mockLogger = new Mock<ILogger<GetContactByDDDUseCase>>();

        var selectedDDD = DDDBrazil.DDD_32;
        Collection<Contact> contacts = [];
        var requestDto = new GetContactByDddDTO(selectedDDD);

        mockRepository
            .Setup(repo => repo.GetContactsByDDDAsync(It.IsAny<DDDBrazil>()))
            .ReturnsAsync(contacts);

        var useCase = new GetContactByDDDUseCase(mockRepository.Object, mockLogger.Object);

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
    public async Task ExecuteAsync_WhenRequestedByDDD_ShouldReturnResultOkWithCollectionWithOneContact()
    {
        // Arrange
        var mockRepository = new Mock<IContactRepository>();
        var mockLogger = new Mock<ILogger<GetContactByDDDUseCase>>();

        var selectedDDD = DDDBrazil.DDD_55;
        var contact = new ContactBuilder().WithDDD(selectedDDD).Build();
        Collection<Contact> contacts = [contact];
        var requestDto = new GetContactByDddDTO(selectedDDD);

        mockRepository
            .Setup(repo => repo.GetContactsByDDDAsync(It.IsAny<DDDBrazil>()))
            .ReturnsAsync(contacts);

        var useCase = new GetContactByDDDUseCase(mockRepository.Object, mockLogger.Object);

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
    public async Task ExecuteAsync_WhenRequestedByDDD_ShouldReturnResultOkWithCollectionWithTwoContacts()
    {
        // Arrange
        var mockRepository = new Mock<IContactRepository>();
        var mockLogger = new Mock<ILogger<GetContactByDDDUseCase>>();

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

        var useCase = new GetContactByDDDUseCase(mockRepository.Object, mockLogger.Object);

        // Act
        var result = await useCase.ExecuteAsync(requestDto);

        // Assert
        result.Should().NotBeNull();
        result.ValueOrDefault.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeEmpty();
        result.Value.Count.Should().Be(contacts.Count);

        for (int i = 0; i < contacts.Count; i++)
        {
            result.Value.ElementAt(i).Id.Should().Be(contacts[i].Id);
            result.Value.ElementAt(i).Name.Should().Be(contacts[i].Name);
            result.Value.ElementAt(i).DDD.Should().Be(contacts[i].DDD);
            result.Value.ElementAt(i).PhoneNumber.Should().Be(contacts[i].PhoneNumber);
            result.Value.ElementAt(i).Email.Should().Be(contacts[i].Email);
        }

        mockRepository.Verify(repo => repo.GetContactsByDDDAsync(selectedDDD), Times.Once);
    }

}
