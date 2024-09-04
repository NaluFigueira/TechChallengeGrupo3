using System;

namespace PosTech.TechChallenge.Users.Application.DTOs;

public class CreateUserDTO
{
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string RePassword { get; set; }
}
