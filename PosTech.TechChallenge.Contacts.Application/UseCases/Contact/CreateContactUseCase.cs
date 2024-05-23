using FluentResults;

using Microsoft.Extensions.Logging;

using PosTech.TechChallenge.Contacts.Domain;
using PosTech.TechChallenge.Contacts.Infra;

namespace PosTech.TechChallenge.Contacts.Application;

public class CreateContactUseCase(IContactRepository contactRepository, ILogger<CreateContactUseCase> logger) : ICreateContactUseCase
{
    private readonly ILogger _logger = logger;
    private readonly IContactRepository _contactRepository = contactRepository;

    public async Task<Result<Contact>> ExecuteAsync(CreateContactDTO request)
    {

        var validationResult = new CreateContactDTOValidator().Validate(request);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage);
            foreach (var error in errors)
            {
                _logger.LogError("[ERR] CreateContactUseCase: {error}", error);
            }
            return Result.Fail(errors);
        }

        var newContact = new Contact
        {
            Name = request.Name,
            DDD = request.DDD,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email
        };

        var contact = await _contactRepository.CreateContactAsync(newContact);

        return Result.Ok(contact);
    }
}
