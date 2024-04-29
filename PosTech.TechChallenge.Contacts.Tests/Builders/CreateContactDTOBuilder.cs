using Bogus;

using PosTech.TechChallenge.Contacts.Application;
using PosTech.TechChallenge.Contacts.Domain;

namespace PosTech.TechChallenge.Contacts.Tests;

public class CreateContactDTOBuilder
{
    public string Name { get; set; }
    public DDDBrazil DDD { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }

    public CreateContactDTOBuilder()
    {
        var Faker = new Faker("pt_BR");
        Name = Faker.Name.FullName();
        DDD = Faker.Random.Enum<DDDBrazil>();
        PhoneNumber = Faker.Random.Replace("9########");
        Email = Faker.Internet.Email();
    }

    public CreateContactDTOBuilder WithName(string name)
    {
        Name = name;
        return this;
    }

    public CreateContactDTOBuilder WithDDD(DDDBrazil ddd)
    {
        DDD = ddd;
        return this;
    }

    public CreateContactDTOBuilder WithPhoneNumber(string phoneNumber)
    {
        PhoneNumber = phoneNumber;
        return this;
    }

    public CreateContactDTOBuilder WithEmail(string email)
    {
        Email = email;
        return this;
    }

    public CreateContactDTO Build()
    {
        return new CreateContactDTO(Name, DDD, PhoneNumber, Email);
    }
}
