using FluentResults;

using Microsoft.Extensions.Logging;

using PosTech.TechChallenge.Contacts.Command.Domain;
using PosTech.TechChallenge.Contacts.Command.Infra;
using PosTech.TechChallenge.Contacts.Command.Infra.Interfaces;
using PosTech.TechChallenge.Contacts.Command.Infra.Queues;

namespace PosTech.TechChallenge.Contacts.Command.Application;

public class CreateContactUseCase(IContactRepository contactRepository, ILogger<CreateContactUseCase> logger, IProducer producer) : ICreateContactUseCase
{
    private readonly ILogger _logger = logger;
    private readonly IContactRepository _contactRepository = contactRepository;
    private readonly IProducer _producer = producer;

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

        try
        {
            _producer.PublishMessageOnQueue(contact, ContactQueues.ContactCreated);
        }
        catch (Exception ex)
        {
            _logger.LogError("[ERROR] Message: {message}", ex.Message);
            _logger.LogError("[ERROR] StackTrace: {stackTrace}", ex.StackTrace);
        }


        return Result.Ok(contact);
    }
}
