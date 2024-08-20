using PosTech.TechChallenge.Contacts.Command.Domain;

namespace PosTech.TechChallenge.Contacts.Command.Application;

public interface ITokenService
{
    string GenerateToken(User user);
}
