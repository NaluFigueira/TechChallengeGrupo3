using FluentResults;

using Microsoft.Extensions.Logging;

using PosTech.TechChallenge.Contacts.Infra;

namespace PosTech.TechChallenge.Contacts.Application;

public class DeleteContactUseCase(IContactRepository contactRepository, ILogger<DeleteContactUseCase> logger) : IDeleteContactUseCase
{
    private readonly ILogger _logger = logger;
    private readonly IContactRepository _contactRepository = contactRepository;

    public async Task<Result> ExecuteAsync(DeleteContactDTO request)
    {
        var validationResult = new DeleteContactDTOValidator().Validate(request);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage);
            foreach (var error in errors)
            {
                _logger.LogError("[ERR] DeleteContactUseCase: {error}", error);
            }
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
