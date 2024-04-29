using FluentResults;

using PosTech.TechChallenge.Contacts.Domain;
using PosTech.TechChallenge.Contacts.Infra;

namespace PosTech.TechChallenge.Contacts.Application;

public class GetContactByDDDUseCase(IContactRepository contactRepository) : IUseCase<GetContactByDddDTO, Result<ICollection<Contact>>>
{
    private readonly IContactRepository _contactRepository = contactRepository;

    public async Task<Result<ICollection<Contact>>> ExecuteAsync(GetContactByDddDTO request)
    {

        try
        {
            var validationResult = new GetContactByDddDTOValidator().Validate(request);
            if (!validationResult.IsValid)
            {
                // usar um log aqui para gerar erros
                var errors = validationResult.Errors.Select(e => e.ErrorMessage);
                return Result.Fail(errors);
            }

            var contacts = await _contactRepository.GetContactsByDDDAsync(request.DDD);

            return Result.Ok(contacts);
        }
        catch (Exception ex)
        {
            // usar um log aqui para gerar erros
            return Result.Fail([ex.Message]);
        }
    }
}
