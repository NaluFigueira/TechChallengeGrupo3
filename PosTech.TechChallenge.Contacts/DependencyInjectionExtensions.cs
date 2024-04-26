using Microsoft.Extensions.DependencyInjection;
using PosTech.TechChallenge.Contacts.Domain;
using PosTech.TechChallenge.Contacts.Services;

namespace PosTech.TechChallenge.Contacts.Infra;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddContactUseCases(this IServiceCollection services)
    {
        services.AddTransient<IUseCase<CreateContactDTO, Result<Contact>>, CreateContactUseCase>();
        services.AddSingleton<CreateContactDTOValidator>();

        return services;
    }
}
