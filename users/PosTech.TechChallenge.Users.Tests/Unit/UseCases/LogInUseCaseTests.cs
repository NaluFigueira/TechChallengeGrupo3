using FluentAssertions;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

using Moq;

using PosTech.TechChallenge.Users.Application.Interfaces.Services;
using PosTech.TechChallenge.Users.Application.UseCases.Authentication;
using PosTech.TechChallenge.Users.Domain.Entities;
using PosTech.TechChallenge.Users.Tests.Builders;

namespace PosTech.TechChallenge.Users.Tests.Unit.UseCases;

public class LogInUseCaseTests
{
    private Mock<SignInManager<User>> _mockSignManager;
    private readonly Mock<ITokenService> _mockTokenService;

    public LogInUseCaseTests()
    {
        var mockUserManager = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);

        _mockSignManager = new Mock<SignInManager<User>>(mockUserManager.Object);

        _mockTokenService = new Mock<ITokenService>();
    }

    [Fact]
    public async Task ExecuteAsync_WhenValidRequest_ShouldReturnOk()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<LogInUseCase>>();

        var request = new LoginDTOBuilder().Build();

        var user = new UserBuilder().WithUserName(request.UserName).WithNormalizedUserName(request.UserName.ToUpper()).Build();

        var mockUserManager = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
        mockUserManager
            .Setup(m => m.Users)
            .Returns(new List<User> { user }.AsQueryable());

        _mockSignManager = new Mock<SignInManager<User>>(mockUserManager.Object, Mock.Of<IHttpContextAccessor>(), Mock.Of<IUserClaimsPrincipalFactory<User>>(), null, null, null, null);

        _mockSignManager
            .Setup(m => m.PasswordSignInAsync(
                            It.IsAny<string>(),
                            It.IsAny<string>(),
                            It.IsAny<bool>(),
                            It.IsAny<bool>()))
            .ReturnsAsync(SignInResult.Success);

        _mockTokenService
            .Setup(m => m.GenerateToken(It.IsAny<User>()))
            .Returns("test");

        var useCase = new LogInUseCase(mockLogger.Object, _mockSignManager.Object, _mockTokenService.Object);

        // Act
        var result = await useCase.ExecuteAsync(request);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNullOrEmpty();

        _mockSignManager.Verify(repo => repo.PasswordSignInAsync(
            It.Is<string>(u => u == request.UserName),
            It.Is<string>(p => p == request.Password),
            It.Is<bool>(isPersistent => isPersistent == false),
            It.Is<bool>(lockoutOnFailure => lockoutOnFailure == false)
        ), Times.Once());

        _mockTokenService.Verify(repo => repo.GenerateToken(It.Is<User>(user => user.UserName == request.UserName)), Times.Once());
    }

    [Fact]
    public async Task ExecuteAsync_WhenManagersFails_ShouldReturnResultFail()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<LogInUseCase>>();

        var request = new LoginDTOBuilder().Build();

        var user = new UserBuilder().WithUserName(request.UserName).WithNormalizedUserName(request.UserName.ToUpper()).Build();

        var mockUserManager = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
        mockUserManager
            .Setup(m => m.Users)
            .Returns(new List<User> { user }.AsQueryable());

        _mockSignManager = new Mock<SignInManager<User>>(mockUserManager.Object, Mock.Of<IHttpContextAccessor>(), Mock.Of<IUserClaimsPrincipalFactory<User>>(), null, null, null, null);

        _mockSignManager
            .Setup(m => m.PasswordSignInAsync(
                            It.IsAny<string>(),
                            It.IsAny<string>(),
                            It.IsAny<bool>(),
                            It.IsAny<bool>()))
            .ReturnsAsync(SignInResult.Failed);

        var useCase = new LogInUseCase(mockLogger.Object, _mockSignManager.Object, _mockTokenService.Object);

        // Act
        var result = await useCase.ExecuteAsync(request);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();

        _mockSignManager.Verify(repo => repo.PasswordSignInAsync(
            It.Is<string>(u => u == request.UserName),
            It.Is<string>(p => p == request.Password),
            It.Is<bool>(isPersistent => isPersistent == false),
            It.Is<bool>(lockoutOnFailure => lockoutOnFailure == false)
        ), Times.Once());
        _mockTokenService.Verify(repo => repo.GenerateToken(It.Is<User>(user => user.UserName == request.UserName)), Times.Never());
    }

    [Fact]
    public async Task ExecuteAsync_WhenUserNotFound_ShouldReturnResultFail()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<LogInUseCase>>();

        var request = new LoginDTOBuilder().Build();

        var mockUserManager = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
        mockUserManager
            .Setup(m => m.Users)
            .Returns(new List<User>().AsQueryable());
        _mockSignManager = new Mock<SignInManager<User>>(mockUserManager.Object, Mock.Of<IHttpContextAccessor>(), Mock.Of<IUserClaimsPrincipalFactory<User>>(), null, null, null, null);

        var useCase = new LogInUseCase(mockLogger.Object, _mockSignManager.Object, _mockTokenService.Object);

        // Act
        var result = await useCase.ExecuteAsync(request);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeFalse();

        _mockSignManager.Verify(repo => repo.PasswordSignInAsync(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<bool>(),
            It.IsAny<bool>()
        ), Times.Never());
        _mockTokenService.Verify(repo => repo.GenerateToken(It.IsAny<User>()), Times.Never());
    }
}
