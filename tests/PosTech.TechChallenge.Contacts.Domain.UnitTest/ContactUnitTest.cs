using FluentValidation.TestHelper;

using PosTech.TechChallenge.Contacts.Domain.Entities;
using PosTech.TechChallenge.Contacts.Domain.Enums;
using PosTech.TechChallenge.Contacts.Validation;

namespace PosTech.TechChallenge.Contacts.Domain.UnitTest;

public class ContactUnitTest
{
    private readonly ContactValidator _validator;

    public ContactUnitTest()
    {
        _validator = new ContactValidator();
    }

    [Fact]
    public void Should_Have_Error_When_Name_Is_Empty()
    {
        var contact = new Contact {
            Name = "",
            DDD = DDDBrazil.DDD55,
            PhoneNumber = "123456789",
            Email = "example@example.com"
        };

        var result = _validator.TestValidate(contact);
        result.ShouldHaveValidationErrorFor(c => c.Name);
        result.ShouldHaveAnyValidationError();
    }

    [Fact]
    public void Should_Have_Error_When_DDD_Is_Empty()
    {
        var contact = new Contact {
            Name = "Example example",
            DDD = (DDDBrazil)0,
            PhoneNumber = "123456789",
            Email = "example@example.com"
        };

        var result = _validator.TestValidate(contact);
        result.ShouldHaveValidationErrorFor(c => c.DDD);
        result.ShouldHaveAnyValidationError();
    }

    [Fact]
    public void Should_Have_Error_When_PhoneNumber_Is_Empty()
    {
        var contact = new Contact {
            Name = "Example example",
            DDD = DDDBrazil.DDD55,
            PhoneNumber = "",
            Email = "example@example.com"
        };

        var result = _validator.TestValidate(contact);
        result.ShouldHaveValidationErrorFor(c => c.PhoneNumber);
        result.ShouldHaveAnyValidationError();
    }

    [Fact]
    public void Should_Have_Error_When_PhoneNumber_Is_Not_Only_Digits()
    {
        var contact = new Contact {
            Name = "Example example",
            DDD = DDDBrazil.DDD55,
            PhoneNumber = "1234-5678",
            Email = "example@example.com"
        };

        var result = _validator.TestValidate(contact);
        result.ShouldHaveValidationErrorFor(c => c.PhoneNumber);
        result.ShouldHaveAnyValidationError();
    }

    [Fact]
    public void Should_Have_Error_When_PhoneNumber_Has_More_Than_9_Digits()
    {
        var contact = new Contact {
            Name = "Example example",
            DDD = DDDBrazil.DDD55,
            PhoneNumber = "55912345678",
            Email = "example@example.com"
        };

        var result = _validator.TestValidate(contact);
        result.ShouldHaveValidationErrorFor(c => c.PhoneNumber);
        result.ShouldHaveAnyValidationError();
    }

    [Fact]
    public void Should_Have_Error_When_PhoneNumber_Has_Less_Than_8_Digits()
    {
        var contact = new Contact {
            Name = "Example example",
            DDD = DDDBrazil.DDD55,
            PhoneNumber = "2345678",
            Email = "example@example.com"
        };

        var result = _validator.TestValidate(contact);
        result.ShouldHaveValidationErrorFor(c => c.PhoneNumber);
        result.ShouldHaveAnyValidationError();
    }

    [Fact]
    public void Should_Have_Error_When_Email_Is_Empty()
    {
        var contact = new Contact {
            Name = "Example example",
            DDD = DDDBrazil.DDD55,
            PhoneNumber = "123456789",
            Email = ""
        };

        var result = _validator.TestValidate(contact);
        result.ShouldHaveValidationErrorFor(c => c.Email);
        result.ShouldHaveAnyValidationError();
    }

    [Fact]
    public void Should_Have_Error_When_Email_Is_Invalid()
    {
        var contact = new Contact {
            Name = "Example example",
            DDD = DDDBrazil.DDD55,
            PhoneNumber = "123456789",
            Email = "Not a valid email"
        };

        var result = _validator.TestValidate(contact);
        result.ShouldHaveValidationErrorFor(c => c.Email);
        result.ShouldHaveAnyValidationError();
    }

    [Fact]
    public void Should_Have_2_Errors_When_Email_And_PhoneNumber_Is_Invalid()
    {
        var contact = new Contact {
            Name = "Example example",
            DDD = DDDBrazil.DDD55,
            PhoneNumber = "+55 (55) 98765-4321",
            Email = ""
        };

        var result = _validator.TestValidate(contact);
        result.ShouldHaveValidationErrorFor(c => c.Email);
        result.ShouldHaveValidationErrorFor(c => c.PhoneNumber);
        result.ShouldHaveAnyValidationError();
    }

    [Fact]
    public void Should_Not_Have_Error_When_All_Fields_Are_Valid()
    {
        var contact = new Contact {
            Name = "Example example",
            DDD = DDDBrazil.DDD55,
            PhoneNumber = "123456789",
            Email = "example@example.com"
        };

        var result = _validator.TestValidate(contact);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
