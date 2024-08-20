using FluentAssertions;

using Microsoft.Extensions.Logging;

using Moq;

using PosTech.TechChallenge.Contacts.Command.Application;
using PosTech.TechChallenge.Contacts.Command.Domain;
using PosTech.TechChallenge.Contacts.Command.Infra;

namespace PosTech.TechChallenge.Contacts.Tests.Unit;

public class UpdateContactDTOUseCaseTests
{
    [Fact]
    public async Task ExecuteAsync_WhenDataIsValid_ShouldReturnResultOk()
    {
        // Arrange
        var mockRepository = new Mock<IContactRepository>();
        var mockLogger = new Mock<ILogger<UpdateContactUseCase>>();

        var contact = new ContactBuilder().Build();

        mockRepository
            .Setup(repo => repo.GetContactByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(contact);

        var request = new UpdateContactDTO
        (
            Id: contact.Id,
            Name: contact.Name,
            DDD: contact.DDD,
            PhoneNumber: contact.PhoneNumber,
            Email: contact.Email
        );
        var updatedContact = new ContactBuilder().WithId(contact.Id).Build();

        mockRepository
            .Setup(repo => repo.UpdateContactAsync(It.IsAny<Contact>()))
            .ReturnsAsync(updatedContact);

        var useCase = new UpdateContactUseCase(mockRepository.Object, mockLogger.Object);

        // Act
        var result = await useCase.ExecuteAsync(request);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();

        // Verify that the repository was called correctly
        mockRepository.Verify(repo => repo.UpdateContactAsync(It.Is<Contact>(c =>
            c.Id == request.Id &&
            c.Name == request.Name &&
            c.DDD == request.DDD &&
            c.PhoneNumber == request.PhoneNumber &&
            c.Email == request.Email
        )), Times.Once());
    }

    [Fact]
    public async Task ExecuteAsync_WhenContactHasInvalidFields_ShouldReturnResultFail()
    {
        // Arrange
        var mockRepository = new Mock<IContactRepository>();
        var mockLogger = new Mock<ILogger<UpdateContactUseCase>>();

        var contact = new ContactBuilder()
            .WithName("")
            .WithEmail("")
            .Build();
        var request = new UpdateContactDTO
        (
            Id: contact.Id,
            Name: contact.Name,
            DDD: contact.DDD,
            PhoneNumber: contact.PhoneNumber, // Invalid phone number
            Email: contact.Email // Invalid Email
        );

        var useCase = new UpdateContactUseCase(mockRepository.Object, mockLogger.Object);

        // Act
        var result = await useCase.ExecuteAsync(request);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();

        mockRepository.Verify(repo => repo.UpdateContactAsync(It.IsAny<Contact>()), Times.Never());
    }
}
