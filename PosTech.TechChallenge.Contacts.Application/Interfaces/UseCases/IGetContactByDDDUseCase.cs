using PosTech.TechChallenge.Contacts.Domain;

namespace PosTech.TechChallenge.Contacts.Application;

public interface IGetContactByDDDUseCase : IUseCase<GetContactByDddDTO, ICollection<Contact>>
{

}
