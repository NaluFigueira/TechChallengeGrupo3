using Moq;

using PosTech.TechChallenge.Contacts.Application.Repositories;
using PosTech.TechChallenge.Contacts.Application.UnitTests.Utilities;
using PosTech.TechChallenge.Contacts.Application.UseCases.UpdateContactByIdUseCase;
using PosTech.TechChallenge.Contacts.Domain.Entities;

public class UpdateContactByIdUseCaseTests
{
    [Fact]
    public async Task ExecuteAsync_ShouldReturnResultOk_WhenDataIsValid()
    {
        // Arrange
        var mockRepository = new Mock<IContactRepository>();

        var contact = new ContactBuilder().Build();
        var request = new UpdateContactByIdRequestDto
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

        var useCase = new UpdateContactByIdUseCase(mockRepository.Object);

        // Act
        var result = await useCase.ExecuteAsync(request);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.ValueOrDefault);
        Assert.True(result.IsSuccess);

        Assert.Equal(updatedContact.Id, result.Value.Id);
        Assert.Equal(updatedContact.Name, result.Value.Name);
        Assert.Equal(updatedContact.DDD, result.Value.DDD);
        Assert.Equal(updatedContact.PhoneNumber, result.Value.PhoneNumber);
        Assert.Equal(updatedContact.Email, result.Value.Email);

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
        var request = new UpdateContactByIdRequestDto
        (
            Id: contact.Id,
            Name: contact.Name,
            DDD: contact.DDD,
            PhoneNumber: contact.PhoneNumber, // Invalid phone number
            Email: contact.Email // Invalid Email
        );

        var useCase = new UpdateContactByIdUseCase(mockRepository.Object);

        // Act
        var result = await useCase.ExecuteAsync(request);

        // Assert
        Assert.NotNull(result);
        Assert.Null(result.ValueOrDefault);
        Assert.False(result.IsSuccess);
        Assert.NotEmpty(result.Errors);

        mockRepository.Verify(repo => repo.UpdateContactAsync(It.IsAny<Contact>()), Times.Never());
    }
}
