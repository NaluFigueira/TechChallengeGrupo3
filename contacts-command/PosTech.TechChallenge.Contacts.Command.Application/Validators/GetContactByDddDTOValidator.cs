using FluentValidation;

namespace PosTech.TechChallenge.Contacts.Command.Application;

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
