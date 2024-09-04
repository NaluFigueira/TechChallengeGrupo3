using System;

namespace PosTech.TechChallenge.Users.Application.DTOs;

public class LoginDTO
{
    public required string UserName { get; set; }
    public required string Password { get; set; }
}
