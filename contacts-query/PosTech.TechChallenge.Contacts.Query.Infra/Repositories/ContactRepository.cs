using Microsoft.EntityFrameworkCore;

using PosTech.TechChallenge.Contacts.Query.Domain.Entities;
using PosTech.TechChallenge.Contacts.Query.Domain.Enum;
using PosTech.TechChallenge.Contacts.Query.Infra.Context;
using PosTech.TechChallenge.Contacts.Query.Infra.Interfaces;

namespace PosTech.TechChallenge.Contacts.Query.Infra.Repositories;

public class ContactRepository(ContactDbContext context) : IContactRepository
{
    protected DbSet<Contact> _dbSet = context.Set<Contact>();

    public async Task<ICollection<Contact>> GetContactsByDDDAsync(DDDBrazil ddd)
    {
        return await _dbSet.Where(x => x.DDD == ddd).AsNoTracking().ToListAsync();
    }
}
