using PosTech.TechChallenge.Contacts.Domain;

namespace PosTech.TechChallenge.Contacts.Application;

public record UpdateContactDTO
{
    public UpdateContactDTO(Guid id, string name, DDDBrazil dDD, string phoneNumber, string email)
    {
        this.Id = id;
        this.Name = name;
        this.DDD = dDD;
        this.PhoneNumber = phoneNumber;
        this.Email = email;
    }

    public Guid Id { get; set; }
    public string? Name { get; set; }
    public DDDBrazil? DDD { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
}
