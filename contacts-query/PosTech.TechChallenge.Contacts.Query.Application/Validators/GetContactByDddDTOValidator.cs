using FluentValidation;

using PosTech.TechChallenge.Contacts.Query.Application.DTOs;

namespace PosTech.TechChallenge.Contacts.Query.Application.Validators;

public class GetContactByDddDTOValidator : AbstractValidator<GetContactByDddDTO>
{
    public GetContactByDddDTOValidator()
    {
        RuleFor(contact => contact.DDD)
            .NotEmpty()
            .WithMessage("DDD is required")
            .IsInEnum()
            .WithMessage("Invalid DDD");
    }


}