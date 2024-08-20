using PosTech.TechChallenge.Contacts.Command.Domain;

namespace PosTech.TechChallenge.Contacts.Command.Application;

public interface IGetContactByDDDUseCase : IUseCase<GetContactByDddDTO, ICollection<Contact>>
{

}
