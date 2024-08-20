using PosTech.TechChallenge.Contacts.Command.Domain;

namespace PosTech.TechChallenge.Contacts.Command.Infra;

public interface IContactRepository
{
    Task<Contact> CreateContactAsync(Contact contact);
    Task DeleteContactAsync(Guid id);
    Task<Contact?> GetContactByIdAsync(Guid id);
    Task<ICollection<Contact>> GetContactsByDDDAsync(DDDBrazil ddd);
    Task<Contact> UpdateContactAsync(Contact contact);
}
