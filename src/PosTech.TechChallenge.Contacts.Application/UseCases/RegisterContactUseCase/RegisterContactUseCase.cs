using FluentResults;

using PosTech.TechChallenge.Contacts.Application.Repositories;
using PosTech.TechChallenge.Contacts.Domain.Entities;

namespace PosTech.TechChallenge.Contacts.Application.UseCases.RegisterContactUseCase;

public class RegisterContactUseCase(IContactRepository contactRepository) : IUseCase<RegisterContactRequestDto, Result<RegisterContactResponseDto>>
{
    private readonly IContactRepository _contactRepository = contactRepository;

    /// <summary>
    /// Validate and add new Contact to the database
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<Result<RegisterContactResponseDto>> ExecuteAsync(
        RegisterContactRequestDto request
    )
    {
        try
        {
            var newContact = new Contact
            {
                Name = request.Name,
                DDD = request.DDD,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email
            };

            var validationResult = newContact.Validate();
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => new Error(e.ErrorMessage));
                return Result.Fail(errors);
            }

            var contact = await _contactRepository.AddContactAsync(newContact);

            var response = new RegisterContactResponseDto(
                Id: contact.Id,
                Name: contact.Name,
                DDD: contact.DDD,
                PhoneNumber: contact.PhoneNumber,
                Email: contact.Email
            );

            return Result.Ok(response);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }
}
