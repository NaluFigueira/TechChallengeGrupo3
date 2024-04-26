using Bogus;

using PosTech.TechChallenge.Contacts.Domain.Entities;
using PosTech.TechChallenge.Contacts.Domain.Enums;

namespace PosTech.TechChallenge.Contacts.Application.UnitTests.Utilities;

public class ContactBuilder
{
    private readonly Faker<Contact> _faker = new Faker<Contact>("pt_BR");

    public ContactBuilder()
    {
        _faker.RuleFor(c => c.Id, f => Guid.NewGuid());
        _faker.RuleFor(c => c.Name, f => f.Name.FullName());
        _faker.RuleFor(c => c.DDD, f => f.PickRandom<DDDBrazil>());
        _faker.RuleFor(c => c.PhoneNumber, f => f.Phone.PhoneNumber("9########"));
        _faker.RuleFor(c => c.Email, f => f.Internet.Email());
    }

    public ContactBuilder WithId(Guid id)
    {
        _faker.RuleFor(c => c.Id, id);
        return this;
    }

    public ContactBuilder WithName(string name)
    {
        _faker.RuleFor(c => c.Name, name);
        return this;
    }

    public ContactBuilder WithDDD(DDDBrazil ddd)
    {
        _faker.RuleFor(c => c.DDD, ddd);
        return this;
    }

    public ContactBuilder WithPhoneNumber(string phoneNumber)
    {
        _faker.RuleFor(c => c.PhoneNumber, phoneNumber);
        return this;
    }

    public ContactBuilder WithEmail(string email)
    {
        _faker.RuleFor(c => c.Email, email);
        return this;
    }

    public Contact Build()
    {
        return _faker.Generate();
    }
}
