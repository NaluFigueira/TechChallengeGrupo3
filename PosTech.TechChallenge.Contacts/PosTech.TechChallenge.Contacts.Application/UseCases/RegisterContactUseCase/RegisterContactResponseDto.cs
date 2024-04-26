using PosTech.TechChallenge.Contacts.Domain.Enums;

namespace PosTech.TechChallenge.Contacts.Application.UseCases.RegisterContactUseCase;

public record RegisterContactResponseDto(
    Guid Id,
    string Name,
    DDDBrazil DDD,
    string PhoneNumber,
    string Email
);
