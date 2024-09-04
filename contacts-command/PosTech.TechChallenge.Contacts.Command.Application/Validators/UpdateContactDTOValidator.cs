using FluentValidation;

namespace PosTech.TechChallenge.Contacts.Command.Application;

public class UpdateContactDTOValidator : AbstractValidator<UpdateContactDTO>
{
    public UpdateContactDTOValidator()
    {
        RuleFor(contact => contact.Id)
            .NotEmpty()
            .WithMessage("Id is required");


        RuleFor(contact => contact.DDD)
            .IsInEnum()
            .WithMessage("Invalid DDD")
            .When(ddd => ddd is not null);

        RuleFor(contact => contact.PhoneNumber)
            .Matches(@"^\d{8,9}$")
            .WithMessage("Phone number must be 8 or 9 digits.")
            .When(phoneNumber => phoneNumber is not null);

        RuleFor(contact => contact.PhoneNumber)
            .Must(PhoneNumberValidations.BeAValidPhoneNumber)
            .WithMessage("Phone number with 9 digits must start with '9'.")
            .When(phoneNumber => phoneNumber is not null);

        When((contact) => contact.Email is not null, () =>
            RuleFor(contact => contact.Email)
                .EmailAddress()
                .WithMessage("A valid email is required.")
        );
    }


}
