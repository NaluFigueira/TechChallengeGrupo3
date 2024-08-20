using FluentAssertions;

using PosTech.TechChallenge.Contacts.Command.Application;

namespace PosTech.TechChallenge.Contacts.Tests.Unit;

public class CreateUserDTOValidatorTests
{
    private readonly CreateUserDTOValidator _validator;

    public CreateUserDTOValidatorTests()
    {
        _validator = new CreateUserDTOValidator();
    }

    [Fact]
    public void Validate_WhenUserNameIsEmpty_ShouldReturnError()
    {
        //Arrange
        var createUserDTO = new CreateUserDTOBuilder().WithUserName("").Build();

        // Act
        var result = _validator.Validate(createUserDTO);

        // Assert
        result.Errors.Should().NotBeEmpty();
        result.Errors.Select(err => err.ErrorMessage).Should().Contain("UserName is required.");
    }

    [Fact]
    public void Validate_WhenUserEmailIsEmpty_ShouldReturnError()
    {
        //Arrange
        var createUserDTO = new CreateUserDTOBuilder().WithEmail("").Build();

        // Act
        var result = _validator.Validate(createUserDTO);

        // Assert
        result.Errors.Should().NotBeEmpty();
        result.Errors.Select(err => err.ErrorMessage).Should().Contain("Email is required.");
    }

    [Fact]
    public void Validate_WhenUsePasswordIsEmpty_ShouldReturnError()
    {
        //Arrange
        var createUserDTO = new CreateUserDTOBuilder().WithPassword("").Build();

        // Act
        var result = _validator.Validate(createUserDTO);

        // Assert
        result.Errors.Should().NotBeEmpty();
        result.Errors.Select(err => err.ErrorMessage).Should().Contain("Password is required.");
    }

    [Fact]
    public void Validate_WhenUseRePasswordIsEmpty_ShouldReturnError()
    {
        //Arrange
        var createUserDTO = new CreateUserDTOBuilder().WithRePassword("").Build();

        // Act
        var result = _validator.Validate(createUserDTO);

        // Assert
        result.Errors.Should().NotBeEmpty();
        result.Errors.Select(err => err.ErrorMessage).Should().Contain("RePassword is required.");
    }

    [Fact]
    public void Validate_WhenUseRePasswordIsDifferentFromPassword_ShouldReturnError()
    {
        //Arrange
        var createUserDTO = new CreateUserDTOBuilder().WithPassword("1").WithRePassword("2").Build();

        // Act
        var result = _validator.Validate(createUserDTO);

        // Assert
        result.Errors.Should().NotBeEmpty();
        result.Errors.Select(err => err.ErrorMessage).Should().Contain("RePassword and Password do not match.");
    }
}
