using FluentResults;

using PosTech.TechChallenge.Contacts.Application.Repositories;

namespace PosTech.TechChallenge.Contacts.Application.UseCases.DeleteContactByIdUseCase;

public class DeleteContactByIdUseCase(IContactRepository contactRepository) : IUseCase<DeleteContactRequestDto, Result>
{
    private readonly IContactRepository _contactRepository = contactRepository;

    /// <summary>
    /// Delete Contact By Id
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<Result> ExecuteAsync(
        DeleteContactRequestDto request
    )
    {
        try
        {
            await _contactRepository.DeleteContactAsync(request.Id);
            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }
}
