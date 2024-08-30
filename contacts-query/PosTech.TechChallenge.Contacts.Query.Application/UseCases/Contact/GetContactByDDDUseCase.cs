using FluentResults;

using Microsoft.Extensions.Logging;

using PosTech.TechChallenge.Contacts.Query.Application.DTOs;
using PosTech.TechChallenge.Contacts.Query.Application.Interfaces.UseCases;
using PosTech.TechChallenge.Contacts.Query.Application.Validators;
using PosTech.TechChallenge.Contacts.Query.Domain.Entities;
using PosTech.TechChallenge.Contacts.Query.Infra.Interfaces;

namespace PosTech.TechChallenge.Contacts.Query.Application.UseCases;

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

