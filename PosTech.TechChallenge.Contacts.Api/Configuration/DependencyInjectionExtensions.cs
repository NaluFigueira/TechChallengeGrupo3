using PosTech.TechChallenge.Contacts.Application;

namespace PosTech.TechChallenge.Contacts.Api.Configuration;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddContactUseCases(this IServiceCollection services)
    {
        services.AddScoped<ICreateContactUseCase, CreateContactUseCase>();
        services.AddScoped<IUpdateContactUseCase, UpdateContactUseCase>();
        services.AddScoped<IGetContactByDDDUseCase, GetContactByDDDUseCase>();
        services.AddScoped<IDeleteContactUseCase, DeleteContactUseCase>();

        return services;
    }

    public static IServiceCollection AddUserUseCases(this IServiceCollection services)
    {
        services.AddScoped<ICreateUserUseCase, CreateUserUseCase>();

        return services;
    }

    public static IServiceCollection AddAuthenticationUseCases(this IServiceCollection services)
    {
        services.AddScoped<ILogInUseCase, LogInUseCase>();

        return services;
    }
}
