using Moq;
using PosTech.TechChallenge.Contacts.Domain;
using PosTech.TechChallenge.Contacts.Infra;
using PosTech.TechChallenge.Contacts.Application;

namespace PosTech.TechChallenge.Contacts.Tests;

public class CreateContactUseCaseTests
{
    [Fact]
    public async Task CreateContactUseCaseTests_ExecuteAsync_ShouldReturnOkWhenValidRequest()
    {
        // Arrange
        var mockRepository = new Mock<IContactRepository>();

        var request = new CreateContactDTOBuilder().Build();
        var expectedContact = new Contact
        {
            Name = request.Name,
            DDD = request.DDD,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email
        };

        mockRepository
            .Setup(repo => repo.CreateContactAsync(It.IsAny<Contact>()))
            .ReturnsAsync(expectedContact);

        var useCase = new CreateContactUseCase(mockRepository.Object);

        // Act
        var result = await useCase.ExecuteAsync(request);

        // Assert
        Assert.NotNull(result.Entity);
        Assert.True(result.IsValid);

        Assert.Equal(expectedContact.Id, result.Entity.Id);
        Assert.Equal(expectedContact.Name, result.Entity.Name);
        Assert.Equal(expectedContact.DDD, result.Entity.DDD);
        Assert.Equal(expectedContact.PhoneNumber, result.Entity.PhoneNumber);
        Assert.Equal(expectedContact.Email, result.Entity.Email);

        mockRepository.Verify(repo => repo.CreateContactAsync(It.Is<Contact>(c =>
            c.Name == request.Name &&
            c.DDD == request.DDD &&
            c.PhoneNumber == request.PhoneNumber &&
            c.Email == request.Email
        )), Times.Once());
    }

    [Fact]
    public async Task CreateContactUseCaseTests_ExecuteAsync_ShouldReturnResultFailWhenContactHasInvalidFields()
    {
        // Arrange
        var mockRepository = new Mock<IContactRepository>();

        var request = new CreateContactDTOBuilder().WithPhoneNumber("").Build();
        var expectedContact = new Contact
        {
            Name = request.Name,
            DDD = request.DDD,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email
        };

        mockRepository
            .Setup(repo => repo.CreateContactAsync(It.IsAny<Contact>()))
            .ThrowsAsync(new Exception("Validation failed."));

        var useCase = new CreateContactUseCase(mockRepository.Object);

        // Act
        var result = await useCase.ExecuteAsync(request);

        // Assert
        Assert.NotNull(result);
        Assert.Null(result.Entity);
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);

        mockRepository.Verify(repo => repo.CreateContactAsync(It.IsAny<Contact>()), Times.Never());
    }
}
