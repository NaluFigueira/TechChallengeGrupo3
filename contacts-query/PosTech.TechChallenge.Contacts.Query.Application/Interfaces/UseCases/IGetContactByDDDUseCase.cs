using PosTech.TechChallenge.Contacts.Query.Application.DTOs;
using PosTech.TechChallenge.Contacts.Query.Domain.Entities;

namespace PosTech.TechChallenge.Contacts.Query.Application.Interfaces.UseCases;

public interface IGetContactByDDDUseCase : IUseCase<GetContactByDddDTO, ICollection<Contact>>
{

}
