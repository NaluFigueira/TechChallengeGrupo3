using FluentValidation;

namespace PosTech.TechChallenge.Contacts.Services;

public class CreateContactDTOValidator : AbstractValidator<CreateContactDTO>
{
    public CreateContactDTOValidator()
    {
        RuleFor(contact => contact.Name)
              .NotEmpty()
              .WithMessage("Name is required.");

        RuleFor(contact => contact.DDD)
            .NotEmpty()
            .WithMessage("DDD is required.");

        RuleFor(contact => contact.PhoneNumber)
            .NotEmpty()
            .WithMessage("Phone number is required.")
            .Matches(@"^\d{8,9}$")
            .WithMessage("Phone number must be 8 or 9 digits.")
            .Must(BeAValidPhoneNumber)
            .WithMessage("Phone number with 9 digits must start with '9'.");

        RuleFor(contact => contact.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("A valid email is required.");
    }

    private bool BeAValidPhoneNumber(string phoneNumber)
    {
        return phoneNumber.Length != 9 || (phoneNumber.Length == 9 && phoneNumber.StartsWith('9'));
    }
}
