using FluentAssertions;

using PosTech.TechChallenge.Contacts.Command.Application;

namespace PosTech.TechChallenge.Contacts.Tests.Unit;

public class CreateContactDTOValidatorTests
{
    private readonly CreateContactDTOValidator _validator;

    public CreateContactDTOValidatorTests()
    {
        _validator = new CreateContactDTOValidator();
    }

    [Fact]
    public void Validate_WhenNameIsEmpty_ShouldReturnError()
    {
        //Arrange
        var createContactDTO = new CreateContactDTOBuilder().WithName("").Build();

        // Act
        var result = _validator.Validate(createContactDTO);

        // Assert
        result.Errors.Should().NotBeEmpty();
        result.Errors.Select(err => err.ErrorMessage).Should().Contain("Name is required.");
    }

    [Fact]
    public void Validate_WhenDDDIsEmpty_ShouldReturnError()
    {
        //Arrange
        var createContactDTO = new CreateContactDTOBuilder().WithDDD(0).Build();

        // Act
        var result = _validator.Validate(createContactDTO);

        // Assert
        result.Errors.Should().NotBeEmpty();
        result.Errors.Select(err => err.ErrorMessage).Should().Contain("Invalid DDD");
    }

    [Theory]
    [InlineData("", "Phone number is required.")]
    [InlineData("1234-5678", "Phone number must be 8 or 9 digits.")]
    [InlineData("55912345678", "Phone number must be 8 or 9 digits.")]
    [InlineData("2345678", "Phone number must be 8 or 9 digits.")]
    [InlineData("888888888", "Phone number with 9 digits must start with '9'.")]
    public void Validate_WhenPhoneNumberIsInvalidFormat_ShouldReturnError(
        string phoneNumber,
        string expectedMessage
    )
    {
        //Arrange
        var createContactDTO = new CreateContactDTOBuilder().WithPhoneNumber(phoneNumber).Build();

        // Act
        var result = _validator.Validate(createContactDTO);

        // Assert
        result.Errors.Should().NotBeEmpty();
        result.Errors.Select(err => err.ErrorMessage).Should().Contain(expectedMessage);
    }

    [Theory]
    [InlineData("", "Email is required.")]
    [InlineData("invalidEmail.com", "A valid email is required.")]
    public void Validate_WhenEmailIsInvalidFormat_ShouldReturnError(
        string email,
        string expectedMessage
    )
    {
        //Arrange
        var createContactDTO = new CreateContactDTOBuilder().WithEmail(email).Build();

        // Act
        var result = _validator.Validate(createContactDTO);

        // Assert
        result.Errors.Should().NotBeEmpty();
        result.Errors.Select(err => err.ErrorMessage).Should().Contain(expectedMessage);
    }


    [Fact]
    public void Validate_WhenFieldIsInvalid_ShouldNotReturnError()
    {
        //Arrange
        var createContactDTO = new CreateContactDTOBuilder().Build();

        // Act
        var result = _validator.Validate(createContactDTO);

        // Assert
        result.Errors.Should().BeEmpty();
    }
}
