using FluentValidation.Results;

using PosTech.TechChallenge.Contacts.Domain.Enums;
using PosTech.TechChallenge.Contacts.Validation;

namespace PosTech.TechChallenge.Contacts.Domain.Entities;

public class Contact
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public required DDDBrazil DDD { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Email { get; set; }

    /// <summary>
    /// Throws Validation error if entity is invalid
    /// </summary>
    public ValidationResult Validate()
    {
        var validator = new ContactValidator();
        var result = validator.Validate(this);
        return result;
    }
}
