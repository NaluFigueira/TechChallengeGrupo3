using Moq;
using PosTech.TechChallenge.Contacts.Command.Domain;
using PosTech.TechChallenge.Contacts.Command.Infra;
using PosTech.TechChallenge.Contacts.Command.Application;
using Microsoft.Extensions.Logging;
using FluentAssertions;


namespace PosTech.TechChallenge.Contacts.Tests.Unit;

public class CreateContactUseCaseTests
{
    [Fact]
    public async Task ExecuteAsync_WhenValidRequest_ShouldReturnOk()
    {
        // Arrange
        var mockRepository = new Mock<IContactRepository>();
        var mockLogger = new Mock<ILogger<CreateContactUseCase>>();

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

        var useCase = new CreateContactUseCase(mockRepository.Object, mockLogger.Object);

        // Act
        var result = await useCase.ExecuteAsync(request);

        // Assert
        result.Value.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();

        result.Value.Id.Should().Be(expectedContact.Id);
        result.Value.Name.Should().Be(expectedContact.Name);
        result.Value.DDD.Should().Be(expectedContact.DDD);
        result.Value.PhoneNumber.Should().Be(expectedContact.PhoneNumber);
        result.Value.Email.Should().Be(expectedContact.Email);

        mockRepository.Verify(repo => repo.CreateContactAsync(It.Is<Contact>(c =>
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
        var mockLogger = new Mock<ILogger<CreateContactUseCase>>();

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

        var useCase = new CreateContactUseCase(mockRepository.Object, mockLogger.Object);

        // Act
        var result = await useCase.ExecuteAsync(request);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();

        mockRepository.Verify(repo => repo.CreateContactAsync(It.IsAny<Contact>()), Times.Never());
    }
}
