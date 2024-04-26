namespace PosTech.TechChallenge.Contacts.Domain;

public class Contact
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public required DDDBrazil DDD { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Email { get; set; }
}
