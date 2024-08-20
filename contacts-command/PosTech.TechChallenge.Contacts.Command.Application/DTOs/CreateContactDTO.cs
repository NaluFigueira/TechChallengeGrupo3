using PosTech.TechChallenge.Contacts.Command.Domain;

namespace PosTech.TechChallenge.Contacts.Command.Application;

public record CreateContactDTO(string Name, DDDBrazil DDD, string PhoneNumber, string Email)
{
    public string Name { get; set; } = Name;
    public DDDBrazil DDD { get; set; } = DDD;
    public string PhoneNumber { get; set; } = PhoneNumber;
    public string Email { get; set; } = Email;
}
