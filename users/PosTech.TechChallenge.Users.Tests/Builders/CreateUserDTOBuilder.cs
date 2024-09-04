using Bogus;

using PosTech.TechChallenge.Users.Application.DTOs;

namespace PosTech.TechChallenge.Users.Tests.Builders;

public class CreateUserDTOBuilder
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string RePassword { get; set; }

    public CreateUserDTOBuilder()
    {
        var faker = new Faker("pt_BR");
        UserName = faker.Internet.UserName();
        Email = faker.Internet.Email();
        Password = faker.Internet.Password();
        RePassword = Password;
    }


    public CreateUserDTOBuilder WithUserName(string username)
    {
        UserName = username;
        return this;
    }

    public CreateUserDTOBuilder WithEmail(string email)
    {
        Email = email;
        return this;
    }

    public CreateUserDTOBuilder WithPassword(string password)
    {
        Password = password;
        return this;
    }

    public CreateUserDTOBuilder WithRePassword(string repassword)
    {
        RePassword = repassword;
        return this;
    }


    public CreateUserDTO Build()
    {
        return new CreateUserDTO
        {
            UserName = UserName,
            Email = Email,
            Password = Password,
            RePassword = RePassword,
        };
    }
}