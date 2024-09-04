using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using PosTech.TechChallenge.Users.Domain.Entities;

namespace PosTech.TechChallenge.Users.Infra.Context;

public class UserDbContext(DbContextOptions<UserDbContext> options) : IdentityDbContext<User>(options)
{
}
