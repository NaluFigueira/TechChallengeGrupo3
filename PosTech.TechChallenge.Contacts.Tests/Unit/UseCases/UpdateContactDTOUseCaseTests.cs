using Moq;

using PosTech.TechChallenge.Contacts.Application;
using PosTech.TechChallenge.Contacts.Domain;
using PosTech.TechChallenge.Contacts.Infra;

namespace PosTech.TechChallenge.Contacts.Tests;

public class UpdateContactDTOUseCaseTests
{
    [Fact]
    public async Task ExecuteAsync_ShouldReturnResultOk_WhenDataIsValid()
    {
        // Arrange
        var mockRepository = new Mock<IContactRepository>();

        var contact = new ContactBuilder().Build();

        mockRepository
            .Setup(repo => repo.GetContactByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(contact);

        var request = new UpdateContactDTO
        (
            id: contact.Id,
            name: contact.Name,
            dDD: contact.DDD,
            phoneNumber: contact.PhoneNumber,
            email: contact.Email
        );
        var updatedContact = new ContactBuilder().WithId(contact.Id).Build();

        mockRepository
            .Setup(repo => repo.UpdateContactAsync(It.IsAny<Contact>()))
            .ReturnsAsync(updatedContact);

        var useCase = new UpdateContactUseCase(mockRepository.Object);

        // Act
        var result = await useCase.ExecuteAsync(request);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);

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
    public async Task ExecuteAsync_ShouldReturnResultFail_WhenContactHasInvalidFields()
    {
        // Arrange
        var mockRepository = new Mock<IContactRepository>();

        var contact = new ContactBuilder()
            .WithName("")
            .WithEmail("")
            .Build();
        var request = new UpdateContactDTO
        (
            id: contact.Id,
            name: contact.Name,
            dDD: contact.DDD,
            phoneNumber: contact.PhoneNumber, // Invalid phone number
            email: contact.Email // Invalid Email
        );

        var useCase = new UpdateContactUseCase(mockRepository.Object);

        // Act
        var result = await useCase.ExecuteAsync(request);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.NotEmpty(result.Errors);

        mockRepository.Verify(repo => repo.UpdateContactAsync(It.IsAny<Contact>()), Times.Never());
    }
}
