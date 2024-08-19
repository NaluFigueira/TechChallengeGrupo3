using PosTech.TechChallenge.Contacts.Domain;

namespace PosTech.TechChallenge.Contacts.Application;

public interface ITokenService
{
    string GenerateToken(User user);
}
