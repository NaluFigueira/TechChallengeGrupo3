using System.Collections.Immutable;

using FluentResults;

using PosTech.TechChallenge.Contacts.Application.Repositories;

namespace PosTech.TechChallenge.Contacts.Application.UseCases.GetContactsByDDDUseCase;

public class GetContactsByDDDUseCase(IContactRepository contactRepository) : IUseCase<GetContactsByDDDRequestDto, Result<GetContactsByDDDResponseDto>>
{
    private readonly IContactRepository _contactRepository = contactRepository;

    /// <summary>
    /// Fetch all contacts by the DDD
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<Result<GetContactsByDDDResponseDto>> ExecuteAsync(
        GetContactsByDDDRequestDto request
    )
    {
        try
        {
            var contacts = await _contactRepository.GetContactsByDDDAsync(request.DDD);

            var response = contacts
                .Select(x => new GetContactsByDDDContactDto(
                    x.Id,
                    x.Name,
                    x.DDD,
                    x.PhoneNumber,
                    x.Email
                ))
                .ToImmutableList();
            var responseList = new GetContactsByDDDResponseDto(Contacts: response);

            return Result.Ok(responseList);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }
}
