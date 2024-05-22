using FluentResults;

using Microsoft.Extensions.Logging;

using PosTech.TechChallenge.Contacts.Domain;
using PosTech.TechChallenge.Contacts.Infra;

namespace PosTech.TechChallenge.Contacts.Application;

public class UpdateContactUseCase(IContactRepository contactRepository, ILogger<UpdateContactUseCase> logger) : IUpdateContactUseCase
{
    private readonly ILogger _logger = logger;
    private readonly IContactRepository _contactRepository = contactRepository;

    public async Task<Result> ExecuteAsync(UpdateContactDTO request)
    {

        var validationResult = new UpdateContactDTOValidator().Validate(request);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage);
            foreach (var error in errors)
            {
                _logger.LogError("[ERR] UpdateContactUseCase: {error}", error);
            }
            return Result.Fail(errors);
        }

        var contact = await _contactRepository.GetContactByIdAsync(request.Id);

        if (contact is null)
        {
            return Result.Fail("Contact not found");
        }

        var updatedContact = new Contact
        {
            Id = request.Id,
            Name = request.Name ?? contact.Name,
            DDD = request.DDD ?? contact.DDD,
            PhoneNumber = request.PhoneNumber ?? contact.PhoneNumber,
            Email = request.Email ?? contact.Email
        };

        await _contactRepository.UpdateContactAsync(updatedContact);

        return Result.Ok();
    }
}
