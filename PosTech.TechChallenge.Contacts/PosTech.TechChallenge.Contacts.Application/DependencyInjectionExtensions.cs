using FluentResults;

using Microsoft.Extensions.DependencyInjection;

using PosTech.TechChallenge.Contacts.Application.UseCases;
using PosTech.TechChallenge.Contacts.Application.UseCases.DeleteContactByIdUseCase;
using PosTech.TechChallenge.Contacts.Application.UseCases.GetContactsByDDDUseCase;
using PosTech.TechChallenge.Contacts.Application.UseCases.RegisterContactUseCase;
using PosTech.TechChallenge.Contacts.Application.UseCases.UpdateContactByIdUseCase;

namespace PosTech.TechChallenge.Contacts.Application;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddContactUseCases(this IServiceCollection services)
    {
        services.AddTransient<IUseCase<DeleteContactRequestDto, Result>, DeleteContactByIdUseCase>();
        services.AddTransient<IUseCase<UpdateContactByIdRequestDto, Result<UpdateContactByIdResponseDto>>, UpdateContactByIdUseCase>();
        services.AddTransient<IUseCase<RegisterContactRequestDto, Result<RegisterContactResponseDto>>, RegisterContactUseCase>();
        services.AddTransient<IUseCase<GetContactsByDDDRequestDto, Result<GetContactsByDDDResponseDto>>, GetContactsByDDDUseCase>();

        return services;
    }
}
