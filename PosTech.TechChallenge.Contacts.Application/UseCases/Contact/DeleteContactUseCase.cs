using FluentResults;

using PosTech.TechChallenge.Contacts.Domain;
using PosTech.TechChallenge.Contacts.Infra;

namespace PosTech.TechChallenge.Contacts.Application;

public class DeleteContactUseCase(IContactRepository contactRepository) : IDeleteContactUseCase
{
    private readonly IContactRepository _contactRepository = contactRepository;

    public async Task<Result> ExecuteAsync(DeleteContactDTO request)
    {
        var validationResult = new DeleteContactDTOValidator().Validate(request);

        if (!validationResult.IsValid)
        {
            // usar um log aqui para gerar erros
            var errors = validationResult.Errors.Select(e => e.ErrorMessage);
            return Result.Fail(errors);
        }

        var contact = await _contactRepository.GetContactByIdAsync(request.Id);

        if (contact is null)
        {
            return Result.Fail("Contact not found");
        }

        await _contactRepository.DeleteContactAsync(request.Id);

        return Result.Ok();
    }
}
