using PosTech.TechChallenge.Contacts.Domain.Enums;

namespace PosTech.TechChallenge.Contacts.Application.UseCases.UpdateContactByIdUseCase;

public record UpdateContactByIdResponseDto(
    Guid Id,
    string Name,
    DDDBrazil DDD,
    string PhoneNumber,
    string Email
);
