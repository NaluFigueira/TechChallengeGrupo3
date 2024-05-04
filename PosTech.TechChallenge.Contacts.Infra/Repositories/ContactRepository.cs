using PosTech.TechChallenge.Contacts.Domain;

namespace PosTech.TechChallenge.Contacts.Infra;

public class ContactRepository : IContactRepository
{
    private List<Contact> _contacts = [];

    public Task<Contact> CreateContactAsync(Contact contact)
    {
        _contacts.Add(contact);
        return Task.FromResult(contact);
    }

    public Task DeleteContactAsync(Guid id)
    {
        _contacts = _contacts.FindAll(x => x.Id != id);
        return Task.CompletedTask;
    }

    public Task<Contact?> GetContactByIdAsync(Guid id)
    {
        var contact = _contacts.Find(x => x.Id == id);
        return Task.FromResult(contact);
    }

    public Task<ICollection<Contact>> GetContactsByDDDAsync(DDDBrazil ddd)
    {
        var contacts = _contacts.FindAll(x => x.DDD == ddd);
        ICollection<Contact> result = contacts;
        return Task.FromResult(result);
    }

    public Task<Contact> UpdateContactAsync(Contact contact)
    {
        var contactIndex = _contacts.FindIndex(x => x.Id == contact.Id);
        _contacts.ElementAt(contactIndex).DDD = contact.DDD;
        _contacts.ElementAt(contactIndex).Name = contact.Name;
        _contacts.ElementAt(contactIndex).Email = contact.Email;
        _contacts.ElementAt(contactIndex).PhoneNumber = contact.PhoneNumber;
        return Task.FromResult(contact);
    }
}
