using System.Diagnostics.Contracts;

using FluentResults;

using PosTech.TechChallenge.Contacts.Domain;
using PosTech.TechChallenge.Contacts.Infra;

namespace PosTech.TechChallenge.Contacts.Application;

public class CreateContactUseCase(IContactRepository contactRepository) : IUseCase<CreateContactDTO, Result<Contact>>
{
    private readonly IContactRepository _contactRepository = contactRepository;

    public async Task<Result<Contact>> ExecuteAsync(CreateContactDTO request)
    {

        try
        {
            var validationResult = new CreateContactDTOValidator().Validate(request);
            if (!validationResult.IsValid)
            {
                // usar um log aqui para gerar erros
                var errors = validationResult.Errors.Select(e => e.ErrorMessage);
                return Result.Fail(errors);
            }

            //Talvez começar a usar um auto mapper pra isso seja interessante
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
        catch (Exception ex)
        {
            // usar um log aqui para gerar erros
            return Result.Fail([ex.Message]);
        }
    }
}
