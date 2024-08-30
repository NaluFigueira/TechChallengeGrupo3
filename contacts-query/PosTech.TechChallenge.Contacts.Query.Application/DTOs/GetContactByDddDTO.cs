using PosTech.TechChallenge.Contacts.Query.Domain.Enum;

namespace PosTech.TechChallenge.Contacts.Query.Application.DTOs;

public class GetContactByDddDTO(DDDBrazil selectedDDD)
{
    public DDDBrazil DDD { get; set; } = selectedDDD;
}
