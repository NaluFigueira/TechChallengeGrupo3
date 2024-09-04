using FluentAssertions;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

using Moq;

using PosTech.TechChallenge.Users.Application.UseCases;
using PosTech.TechChallenge.Users.Domain.Entities;
using PosTech.TechChallenge.Users.Tests.Builders;

namespace PosTech.TechChallenge.Users.Tests.Unit.UseCases;

public class CreateUserUseCaseTests
{
    private readonly Mock<UserManager<User>> _mockUserManager = new(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);

    [Fact]
    public async Task ExecuteAsync_WhenValidRequest_ShouldReturnOk()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<CreateUserUseCase>>();

        var request = new CreateUserDTOBuilder().Build();
        var expectedUser = new User
        {
            UserName = request.UserName,
            Email = request.Email,
        };

        _mockUserManager
            .Setup(repo => repo.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);

        var useCase = new CreateUserUseCase(mockLogger.Object, _mockUserManager.Object);

        // Act
        var result = await useCase.ExecuteAsync(request);

        // Assert
        result.IsSuccess.Should().BeTrue();

        _mockUserManager.Verify(repo => repo.CreateAsync(It.Is<User>(c =>
            c.Email == request.Email &&
            c.UserName == request.UserName
        ), It.Is<string>(s => s == request.Password)), Times.Once());
    }

    [Fact]
    public async Task ExecuteAsync_WhenUserHasInvalidFields_ShouldReturnResultFail()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<CreateUserUseCase>>();

        var request = new CreateUserDTOBuilder().WithPassword("1").WithRePassword("2").Build();

        _mockUserManager
            .Setup(repo => repo.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);

        var useCase = new CreateUserUseCase(mockLogger.Object, _mockUserManager.Object);

        // Act
        var result = await useCase.ExecuteAsync(request);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();

        _mockUserManager.Verify(repo => repo.CreateAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Never());
    }

    [Fact]
    public async Task ExecuteAsync_WhenDatabaseInsertionFails_ShouldReturnResultFail()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<CreateUserUseCase>>();

        var request = new CreateUserDTOBuilder().Build();

        _mockUserManager
            .Setup(repo => repo.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Failed([new IdentityError()]));

        var useCase = new CreateUserUseCase(mockLogger.Object, _mockUserManager.Object);

        // Act
        var result = await useCase.ExecuteAsync(request);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().NotBeEmpty();

        _mockUserManager.Verify(repo => repo.CreateAsync(It.Is<User>(c =>
            c.Email == request.Email &&
            c.UserName == request.UserName
        ), It.Is<string>(s => s == request.Password)), Times.Once());
    }
}
