using PosTech.TechChallenge.Users.Application.Interfaces.UseCases;
using PosTech.TechChallenge.Users.Application.UseCases.Authentication;
using PosTech.TechChallenge.Users.Application.UseCases;

namespace PosTech.TechChallenge.Users.Api.Configuration;

public static class DependencyInjectionExtensions
{
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
