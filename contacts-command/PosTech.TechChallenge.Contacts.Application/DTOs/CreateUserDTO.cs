namespace PosTech.TechChallenge.Contacts.Application;

public class CreateUserDTO
{
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string RePassword { get; set; }
}
