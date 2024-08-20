using Microsoft.EntityFrameworkCore;

using PosTech.TechChallenge.Contacts.Command.Domain;
using PosTech.TechChallenge.Contacts.Command.Infra.Context;

namespace PosTech.TechChallenge.Contacts.Command.Infra;

public class ContactRepository : IContactRepository
{
    protected AplicationDbContext _context;
    protected DbSet<Contact> _dbSet;

    public ContactRepository(AplicationDbContext context)
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
        _dbSet.Remove(await GetContactByIdAsync(id));
        await _context.SaveChangesAsync();
    }

    public async Task<Contact?> GetContactByIdAsync(Guid id)
    {
        return await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<ICollection<Contact>> GetContactsByDDDAsync(DDDBrazil ddd)
    {
        return await _dbSet.Where(x => x.DDD == ddd).AsNoTracking().ToListAsync();
    }

    public async Task<Contact> UpdateContactAsync(Contact contact)
    {
        _dbSet.Update(contact);
        await _context.SaveChangesAsync();
        return contact;
    }
}
