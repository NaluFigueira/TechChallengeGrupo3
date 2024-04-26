using PosTech.TechChallenge.Contacts.Domain.Entities;
using PosTech.TechChallenge.Contacts.Domain.Enums;

namespace PosTech.TechChallenge.Contacts.Application.Repositories;

public interface IContactRepository
{
    Task<Contact> AddContactAsync(Contact contact);
    Task DeleteContactAsync(Guid id);
    Task<ICollection<Contact>> GetContactsByDDDAsync(DDDBrazil ddd);
    Task<Contact> UpdateContactAsync(Contact contact);
}
