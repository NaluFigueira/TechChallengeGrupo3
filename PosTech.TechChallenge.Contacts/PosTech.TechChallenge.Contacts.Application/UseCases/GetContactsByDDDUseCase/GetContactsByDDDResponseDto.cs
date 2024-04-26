using System.Collections.Immutable;

using PosTech.TechChallenge.Contacts.Domain.Enums;

namespace PosTech.TechChallenge.Contacts.Application.UseCases.GetContactsByDDDUseCase;

public record GetContactsByDDDContactDto(
    Guid Id,
    string Name,
    DDDBrazil DDD,
    string PhoneNumber,
    string Email
);

public record GetContactsByDDDResponseDto(
    ImmutableList<GetContactsByDDDContactDto> Contacts
);
