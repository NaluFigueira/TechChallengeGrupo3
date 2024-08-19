using FluentValidation;

namespace PosTech.TechChallenge.Contacts.Application;

public class CreateContactDTOValidator : AbstractValidator<CreateContactDTO>
{
    public CreateContactDTOValidator()
    {
        RuleFor(contact => contact.Name)
            .NotEmpty()
            .WithMessage("Name is required.");

        RuleFor(contact => contact.DDD)
            .NotEmpty()
            .WithMessage("DDD is required")
            .IsInEnum()
            .WithMessage("Invalid DDD");

        RuleFor(contact => contact.PhoneNumber)
            .NotEmpty()
            .WithMessage("Phone number is required.")
            .Matches(@"^\d{8,9}$")
            .WithMessage("Phone number must be 8 or 9 digits.")
            .Must(PhoneNumberValidations.BeAValidPhoneNumber)
            .WithMessage("Phone number with 9 digits must start with '9'.");

        RuleFor(contact => contact.Email)
            .NotEmpty()
            .WithMessage("Email is required.");

        RuleFor(contact => contact.Email)
            .EmailAddress()
            .WithMessage("A valid email is required.");
    }
}
