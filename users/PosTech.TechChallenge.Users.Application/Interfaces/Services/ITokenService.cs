using PosTech.TechChallenge.Users.Domain.Entities;

namespace PosTech.TechChallenge.Users.Application.Interfaces.Services;

public interface ITokenService
{
    string GenerateToken(User user);
}
