using PosTech.TechChallenge.Contacts.Query.Application.Interfaces.UseCases;
using PosTech.TechChallenge.Contacts.Query.Application.UseCases;

namespace PosTech.TechChallenge.Contacts.Query.Api.Configuration;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddContactUseCases(this IServiceCollection services)
    {
        services.AddScoped<IGetContactByDDDUseCase, GetContactByDDDUseCase>();

        return services;
    }
}
