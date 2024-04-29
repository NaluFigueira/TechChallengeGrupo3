using FluentResults;

using PosTech.TechChallenge.Contacts.Domain;
using PosTech.TechChallenge.Contacts.Infra;

namespace PosTech.TechChallenge.Contacts.Application;

public class DeleteContactUseCase(IContactRepository contactRepository) : IUseCase<DeleteContactDTO, Result>
{
    private readonly IContactRepository _contactRepository = contactRepository;

    public async Task<Result> ExecuteAsync(DeleteContactDTO request)
    {

        try
        {
            var validationResult = new DeleteContactDTOValidator().Validate(request);
            if (!validationResult.IsValid)
            {
                // usar um log aqui para gerar erros
                var errors = validationResult.Errors.Select(e => e.ErrorMessage);
                return Result.Fail(errors);
            }

            await _contactRepository.DeleteContactAsync(request.Id);

            return Result.Ok();
        }
        catch (Exception ex)
        {
            // usar um log aqui para gerar erros
            return Result.Fail([ex.Message]);
        }
    }
}
