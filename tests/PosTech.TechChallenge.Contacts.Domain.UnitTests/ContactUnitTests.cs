using FluentValidation.TestHelper;

using PosTech.TechChallenge.Contacts.Domain.Entities;
using PosTech.TechChallenge.Contacts.Domain.Enums;
using PosTech.TechChallenge.Contacts.Validation;

namespace PosTech.TechChallenge.Contacts.Domain.UnitTests;

public class ContactUnitTests
{
    private readonly ContactValidator _validator;

    public ContactUnitTests()
    {
        _validator = new ContactValidator();
    }

    [Fact]
    public void Should_Have_Id_When_New_Instance_Is_Created()
    {
        var contact = new Contact
        {
            Name = "Exemple Exemple",
            DDD = DDDBrazil.DDD55,
            PhoneNumber = "123456789",
            Email = "example@example.com"
        };

        Assert.IsType<Guid>(contact.Id);
        Assert.NotEqual(contact.Id, Guid.Empty);
    }

    [Fact]
    public void Should_Have_Error_When_Name_Is_Empty()
    {
        var contact = new Contact
        {
            Name = "",
            DDD = DDDBrazil.DDD55,
            PhoneNumber = "123456789",
            Email = "example@example.com"
        };

        var result = _validator.TestValidate(contact);
        result.ShouldHaveAnyValidationError();
        result.ShouldHaveValidationErrorFor(c => c.Name)
            .WithErrorMessage("Name is required.");
    }

    [Fact]
    public void Should_Have_Error_When_DDD_Is_Empty()
    {
        var contact = new Contact
        {
            Name = "Example Example",
            DDD = (DDDBrazil)0,
            PhoneNumber = "123456789",
            Email = "example@example.com"
        };

        var result = _validator.TestValidate(contact);
        result.ShouldHaveAnyValidationError();
        result.ShouldHaveValidationErrorFor(c => c.DDD)
            .WithErrorMessage("DDD is required.");
    }

    [Theory]
    [InlineData("", "Phone number is required.")]
    [InlineData("1234-5678", "Phone number must be 8 or 9 digits.")]
    [InlineData("55912345678", "Phone number must be 8 or 9 digits.")]
    [InlineData("2345678", "Phone number must be 8 or 9 digits.")]
    [InlineData("888888888", "Phone number with 9 digits must start with '9'.")]
    public void Should_Have_Error_For_Invalid_PhoneNumber_Formats(
        string phoneNumber,
        string expectedMessage
    )
    {
        var contact = new Contact
        {
            Name = "Example Example",
            DDD = DDDBrazil.DDD55,
            PhoneNumber = phoneNumber,
            Email = "example@example.com"
        };

        var result = _validator.TestValidate(contact);
        result.ShouldHaveAnyValidationError();
        result.ShouldHaveValidationErrorFor(c => c.PhoneNumber)
            .WithErrorMessage(expectedMessage);
    }

    [Theory]
    [InlineData("", "Email is required.")]
    [InlineData("invalidEmail.com", "A valid email is required.")]
    public void Should_Have_Error_For_Invalid_Email_Formats(
        string email,
        string expectedMessage
    )
    {
        var contact = new Contact
        {
            Name = "Example Example",
            DDD = DDDBrazil.DDD55,
            PhoneNumber = "123456789",
            Email = email
        };

        var result = _validator.TestValidate(contact);
        result.ShouldHaveAnyValidationError();
        result.ShouldHaveValidationErrorFor(c => c.Email)
            .WithErrorMessage(expectedMessage);
    }

    [Fact]
    public void Should_Have_2_Errors_When_Email_And_PhoneNumber_Is_Invalid()
    {
        var contact = new Contact
        {
            Name = "Example Example",
            DDD = DDDBrazil.DDD55,
            PhoneNumber = "+55 (55) 98765-4321",
            Email = ""
        };

        var result = _validator.TestValidate(contact);
        result.ShouldHaveValidationErrorFor(c => c.Email);
        result.ShouldHaveValidationErrorFor(c => c.PhoneNumber);
        result.ShouldHaveAnyValidationError();
    }

    [Theory]
    [InlineData("John Doe", DDDBrazil.DDD11, "987654321", "john.doe@example.com")]
    [InlineData("Alice Johnson", DDDBrazil.DDD21, "976543219", "alice.johnson@example.net")]
    [InlineData("Bob Brown", DDDBrazil.DDD31, "965432198", "bob.brown@example.org")]
    [InlineData("Clara Sky", DDDBrazil.DDD41, "954321987", "clara.sky@example.co.uk")]
    [InlineData("Daniel Moon", DDDBrazil.DDD51, "943219876", "d.moon@moonlight.io")]
    [InlineData("Eva Storm", DDDBrazil.DDD61, "932198765", "eva.storm@cloud.com")]
    [InlineData("Finn Gale", DDDBrazil.DDD71, "921987654", "finn.gale@example.com")]
    [InlineData("Grace Field", DDDBrazil.DDD81, "919876543", "grace.field@fields.net")]
    [InlineData("Hector Sage", DDDBrazil.DDD91, "998765432", "hector.sage@sage.org")]
    [InlineData("Isla Frost", DDDBrazil.DDD19, "923456789", "isla.frost@frost.co.uk")]
    public void Should_Not_Have_Error_When_All_Fields_Are_Valid(
        string name,
        DDDBrazil ddd,
        string phoneNumber,
        string email
    )
    {
        var contact = new Contact
        {
            Name = name,
            DDD = ddd,
            PhoneNumber = phoneNumber,
            Email = email
        };

        var result = _validator.TestValidate(contact);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
