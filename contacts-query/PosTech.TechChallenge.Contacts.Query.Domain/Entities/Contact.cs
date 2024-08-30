using PosTech.TechChallenge.Contacts.Query.Domain.Enum;

namespace PosTech.TechChallenge.Contacts.Query.Domain.Entities;

public class Contact
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public required DDDBrazil DDD { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Email { get; set; }
}
