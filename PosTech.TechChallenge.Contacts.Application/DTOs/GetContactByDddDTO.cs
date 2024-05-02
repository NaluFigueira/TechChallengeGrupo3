using PosTech.TechChallenge.Contacts.Domain;

namespace PosTech.TechChallenge.Contacts.Application;

public record GetContactByDddDTO
{
    public GetContactByDddDTO(DDDBrazil selectedDDD)
    {
        DDD = selectedDDD;
    }

    public DDDBrazil DDD { get; set; }
}
