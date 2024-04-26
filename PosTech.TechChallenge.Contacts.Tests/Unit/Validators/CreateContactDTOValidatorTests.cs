using PosTech.TechChallenge.Contacts.Services;

namespace PosTech.TechChallenge.Contacts.Tests;

public class CreateContactDTOValidatorTests
{
    private readonly CreateContactDTOValidator _validator;

    public CreateContactDTOValidatorTests()
    {
        _validator = new CreateContactDTOValidator();
    }

    [Fact]
    public void CreateContactDTOValidator_Validate_ShouldReturnErrorWhenNameIsEmpty()
    {
        //Arrange
        var createContactDTO = new CreateContactDTOBuilder().WithName("").Build();

        // Act
        var result = _validator.Validate(createContactDTO);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.Contains("Name is required.", result.Errors.Select(err => err.ErrorMessage));
    }

    [Fact]
    public void CreateContactDTOValidator_Validate_ShouldReturnErrorWhenDDDIsEmpty()
    {
        //Arrange
        var createContactDTO = new CreateContactDTOBuilder().WithDDD(0).Build();

        // Act
        var result = _validator.Validate(createContactDTO);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.Contains("DDD is required.", result.Errors.Select(err => err.ErrorMessage));
    }

    [Theory]
    [InlineData("", "Phone number is required.")]
    [InlineData("1234-5678", "Phone number must be 8 or 9 digits.")]
    [InlineData("55912345678", "Phone number must be 8 or 9 digits.")]
    [InlineData("2345678", "Phone number must be 8 or 9 digits.")]
    [InlineData("888888888", "Phone number with 9 digits must start with '9'.")]
    public void CreateContactDTOValidator_Validate_ShouldReturnErrorWhenPhoneNumberIsInvalidFormat(
        string phoneNumber,
        string expectedMessage
    )
    {
        //Arrange
        var createContactDTO = new CreateContactDTOBuilder().WithPhoneNumber(phoneNumber).Build();

        // Act
        var result = _validator.Validate(createContactDTO);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.Contains(expectedMessage, result.Errors.Select(err => err.ErrorMessage));
    }

    [Theory]
    [InlineData("", "Email is required.")]
    [InlineData("invalidEmail.com", "A valid email is required.")]
    public void CreateContactDTOValidator_Validate_ShouldReturnErrorWhenEmailIsInvalidFormat(
        string email,
        string expectedMessage
    )
    {
        //Arrange
        var createContactDTO = new CreateContactDTOBuilder().WithEmail(email).Build();

        // Act
        var result = _validator.Validate(createContactDTO);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.Contains(expectedMessage, result.Errors.Select(err => err.ErrorMessage));
    }


    [Fact]
    public void CreateContactDTOValidator_Validate_ShouldNotReturnErrorWhenFieldIsInvalid()
    {
        //Arrange
        var createContactDTO = new CreateContactDTOBuilder().Build();

        // Act
        var result = _validator.Validate(createContactDTO);

        // Assert
        Assert.Empty(result.Errors);
    }
}
