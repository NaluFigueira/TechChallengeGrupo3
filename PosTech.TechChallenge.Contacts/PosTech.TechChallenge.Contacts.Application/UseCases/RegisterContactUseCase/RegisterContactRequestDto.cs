using PosTech.TechChallenge.Contacts.Domain.Enums;

namespace PosTech.TechChallenge.Contacts.Application.UseCases.RegisterContactUseCase;

public record RegisterContactRequestDto(
    string Name,
    DDDBrazil DDD,
    string PhoneNumber,
    string Email
);
