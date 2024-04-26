using FluentResults;

using PosTech.TechChallenge.Contacts.Application.Repositories;
using PosTech.TechChallenge.Contacts.Domain.Entities;

namespace PosTech.TechChallenge.Contacts.Application.UseCases.UpdateContactByIdUseCase;

public class UpdateContactByIdUseCase(IContactRepository contactRepository) : IUseCase<UpdateContactByIdRequestDto, Result<UpdateContactByIdResponseDto>>
{
    private readonly IContactRepository _contactRepository = contactRepository;

    /// <summary>
    /// Update Contact
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<Result<UpdateContactByIdResponseDto>> ExecuteAsync(
        UpdateContactByIdRequestDto request
    )
    {
        try
        {
            var updatedContact = new Contact
            {
                Id = request.Id,
                Name = request.Name,
                DDD = request.DDD,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email
            };

            var validationResult = updatedContact.Validate();
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => new Error(e.ErrorMessage));
                return Result.Fail(errors);
            }

            var contact = await _contactRepository.UpdateContactAsync(updatedContact);

            var response = new UpdateContactByIdResponseDto(
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
