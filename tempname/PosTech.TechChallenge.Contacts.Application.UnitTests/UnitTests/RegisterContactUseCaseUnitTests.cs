using FluentValidation;

using Moq;

using PosTech.TechChallenge.Contacts.Application.Repositories;
using PosTech.TechChallenge.Contacts.Application.UnitTests.Utilities;
using PosTech.TechChallenge.Contacts.Application.UseCases.RegisterContactUseCase;
using PosTech.TechChallenge.Contacts.Domain.Entities;

namespace PosTech.TechChallenge.Contacts.Application.UnitTests;

public class RegisterContactUseCaseTests
{
    [Fact]
    public async Task ExecuteAsync_ShouldReturnResultOk_WhenValidRequest()
    {
        // Arrange
        var mockRepository = new Mock<IContactRepository>();

        var expectedContact = new ContactBuilder().Build();
        var request = new RegisterContactRequestDto(
            Name: expectedContact.Name,
            DDD: expectedContact.DDD,
            PhoneNumber: expectedContact.PhoneNumber,
            Email: expectedContact.Email
        );

        mockRepository
            .Setup(repo => repo.AddContactAsync(It.IsAny<Contact>()))
            .ReturnsAsync(expectedContact);

        var useCase = new RegisterContactUseCase(mockRepository.Object);

        // Act
        var result = await useCase.ExecuteAsync(request);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.ValueOrDefault);
        Assert.True(result.IsSuccess);

        Assert.Equal(expectedContact.Id, result.Value.Id);
        Assert.Equal(expectedContact.Name, result.Value.Name);
        Assert.Equal(expectedContact.DDD, result.Value.DDD);
        Assert.Equal(expectedContact.PhoneNumber, result.Value.PhoneNumber);
        Assert.Equal(expectedContact.Email, result.Value.Email);

        mockRepository.Verify(repo => repo.AddContactAsync(It.Is<Contact>(c =>
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
            .WithPhoneNumber("")
            .Build();
        var request = new RegisterContactRequestDto
        (
            Name: contact.Name,
            DDD: contact.DDD,
            PhoneNumber: contact.PhoneNumber,  // Invalid phone number
            Email: contact.Email
        );

        mockRepository
            .Setup(repo => repo.AddContactAsync(It.IsAny<Contact>()))
            .ThrowsAsync(new ValidationException("Validation failed."));

        var useCase = new RegisterContactUseCase(mockRepository.Object);

        // Act
        var result = await useCase.ExecuteAsync(request);

        // Assert
        Assert.NotNull(result);
        Assert.Null(result.ValueOrDefault);
        Assert.False(result.IsSuccess);
        Assert.NotEmpty(result.Errors);

        mockRepository.Verify(repo => repo.AddContactAsync(It.IsAny<Contact>()), Times.Never());
    }
}
