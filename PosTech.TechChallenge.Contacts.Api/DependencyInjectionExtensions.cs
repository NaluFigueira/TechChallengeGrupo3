using PosTech.TechChallenge.Contacts.Application;
using PosTech.TechChallenge.Contacts.Infra;

namespace PosTech.TechChallenge.Contacts.Api;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddContactUseCases(this IServiceCollection services)
    {
        services.AddScoped<ICreateContactUseCase, CreateContactUseCase>();
        services.AddScoped<IUpdateContactUseCase, UpdateContactUseCase>();
        services.AddScoped<IGetContactByDDDUseCase, GetContactByDDDUseCase>();
        services.AddScoped<IDeleteContactUseCase, DeleteContactUseCase>();
        services.AddScoped<IContactRepository, ContactRepository>();

        return services;
    }
}
