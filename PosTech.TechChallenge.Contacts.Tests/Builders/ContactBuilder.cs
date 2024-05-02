using Bogus;

using PosTech.TechChallenge.Contacts.Domain;

namespace PosTech.TechChallenge.Contacts.Tests;

public class ContactBuilder
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DDDBrazil DDD { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }

    public ContactBuilder()
    {
        var Faker = new Faker("pt_BR");
        Id = Guid.NewGuid();
        Name = Faker.Name.FullName();
        DDD = Faker.Random.Enum<DDDBrazil>();
        PhoneNumber = Faker.Random.Replace("9########");
        Email = Faker.Internet.Email();
    }

    public ContactBuilder WithId(Guid id)
    {
        Id = id;
        return this;
    }

    public ContactBuilder WithName(string name)
    {
        Name = name;
        return this;
    }

    public ContactBuilder WithDDD(DDDBrazil ddd)
    {
        DDD = ddd;
        return this;
    }

    public ContactBuilder WithPhoneNumber(string phoneNumber)
    {
        PhoneNumber = phoneNumber;
        return this;
    }

    public ContactBuilder WithEmail(string email)
    {
        Email = email;
        return this;
    }

    public Contact Build()
    {
        return new Contact
        {
            Name = this.Name,
            DDD = this.DDD,
            PhoneNumber = this.PhoneNumber,
            Email = this.Email
        };
    }
}
