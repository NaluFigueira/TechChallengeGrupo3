using Microsoft.EntityFrameworkCore;

using PosTech.TechChallenge.Contacts.Command.Domain;
using PosTech.TechChallenge.Contacts.Command.Infra.Context;

namespace PosTech.TechChallenge.Contacts.Command.Infra;

public class ContactRepository : IContactRepository
{
    protected ContactDbContext _context;
    protected DbSet<Contact> _dbSet;

    public ContactRepository(ContactDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<Contact>();
    }

    public async Task<Contact> CreateContactAsync(Contact contact)
    {
        await _dbSet.AddAsync(contact);
        await _context.SaveChangesAsync();
        return contact;
    }

    public async Task DeleteContactAsync(Guid id)
    {
        var contact = await GetContactByIdAsync(id);

        if (contact is not null)
        {
            _dbSet.Remove(contact);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Contact?> GetContactByIdAsync(Guid id)
    {
        return await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Contact> UpdateContactAsync(Contact contact)
    {
        _dbSet.Update(contact);
        await _context.SaveChangesAsync();
        return contact;
    }
}
