using FluentValidation;

namespace PosTech.TechChallenge.Contacts.Application;

public class DeleteContactDTOValidator : AbstractValidator<DeleteContactDTO>
{
    public DeleteContactDTOValidator()
    {
        RuleFor(contact => contact.Id)
            .NotEmpty()
            .WithMessage("Id is required");
    }
}
