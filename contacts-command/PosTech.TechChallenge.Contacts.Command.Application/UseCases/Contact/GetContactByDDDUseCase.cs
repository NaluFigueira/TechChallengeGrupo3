using FluentResults;

using Microsoft.Extensions.Logging;

using PosTech.TechChallenge.Contacts.Command.Domain;
using PosTech.TechChallenge.Contacts.Command.Infra;

namespace PosTech.TechChallenge.Contacts.Command.Application;

public class GetContactByDDDUseCase(IContactRepository contactRepository, ILogger<GetContactByDDDUseCase> logger) : IGetContactByDDDUseCase
{
    private readonly ILogger _logger = logger;
    private readonly IContactRepository _contactRepository = contactRepository;

    public async Task<Result<ICollection<Contact>>> ExecuteAsync(GetContactByDddDTO request)
    {
        var validationResult = new GetContactByDddDTOValidator().Validate(request);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage);
            foreach (var error in errors)
            {
                _logger.LogError("[ERR] GetContactByDDDUseCase: {error}", error);
            }
            return Result.Fail(errors);
        }

        var contacts = await _contactRepository.GetContactsByDDDAsync(request.DDD);

        return Result.Ok(contacts);
    }
}
