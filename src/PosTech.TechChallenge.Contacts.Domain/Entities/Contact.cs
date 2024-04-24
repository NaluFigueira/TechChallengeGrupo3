using FluentValidation;

using PosTech.TechChallenge.Contacts.Domain.Enums;
using PosTech.TechChallenge.Contacts.Validation;

namespace PosTech.TechChallenge.Contacts.Domain.Entities;

public class Contact
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required DDDBrazil DDD { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Email { get; set; }

    public Contact() { }

    public Contact(string name, DDDBrazil ddd, string phoneNumber, string email)
    {
        Id = Guid.NewGuid();
        Name = name;
        DDD = ddd;
        PhoneNumber = phoneNumber;
        Email = email;

        Validate();
    }

    private void Validate()
    {
        var validator = new ContactValidator();
        var result = validator.Validate(this);
        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors);
        }
    }
}
