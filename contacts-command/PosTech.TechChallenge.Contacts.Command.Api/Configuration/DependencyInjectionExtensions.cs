using PosTech.TechChallenge.Contacts.Command.Application;

namespace PosTech.TechChallenge.Contacts.Command.Api.Configuration;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddContactUseCases(this IServiceCollection services)
    {
        services.AddScoped<ICreateContactUseCase, CreateContactUseCase>();
        services.AddScoped<IUpdateContactUseCase, UpdateContactUseCase>();
        services.AddScoped<IDeleteContactUseCase, DeleteContactUseCase>();

        return services;
    }
}
