using PosTech.TechChallenge.Contacts.Command.Domain;

namespace PosTech.TechChallenge.Contacts.Command.Application;

public record GetContactByDddDTO
{
    public GetContactByDddDTO(DDDBrazil selectedDDD)
    {
        DDD = selectedDDD;
    }

    public DDDBrazil DDD { get; set; }
}
