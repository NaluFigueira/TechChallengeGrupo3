using FluentValidation;

using PosTech.TechChallenge.Contacts.Domain.Entities;

namespace PosTech.TechChallenge.Contacts.Validation;

public class ContactValidator : AbstractValidator<Contact>
{
    public ContactValidator()
    {
        RuleFor(contact => contact.Name)
            .NotEmpty()
            .WithMessage("O nome do contato é obrigatório.");

        RuleFor(contact => contact.DDD)
            .NotEmpty()
            .WithMessage("O DDD é obrigatório.");

        RuleFor(contact => contact.PhoneNumber)
            .NotEmpty()
            .WithMessage("O número de telefone é obrigatório.")
            .Matches(@"^\d{8,9}$")
            .WithMessage("O número de telefone deve conter de 8 a 9 dígitos.");

        RuleFor(contact => contact.Email)
            .NotEmpty()
            .WithMessage("O e-mail é obrigatório.")
            .EmailAddress()
            .WithMessage("O e-mail fornecido não é válido.");
    }
}
