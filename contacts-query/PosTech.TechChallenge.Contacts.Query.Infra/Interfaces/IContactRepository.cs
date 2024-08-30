using System;

using PosTech.TechChallenge.Contacts.Query.Domain.Entities;
using PosTech.TechChallenge.Contacts.Query.Domain.Enum;

namespace PosTech.TechChallenge.Contacts.Query.Infra.Interfaces;

public interface IContactRepository
{
    Task<ICollection<Contact>> GetContactsByDDDAsync(DDDBrazil ddd);
}
