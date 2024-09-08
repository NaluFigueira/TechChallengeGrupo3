using PosTech.TechChallenge.Contacts.Query.Infra.Consumers;

namespace PosTech.TechChallenge.Contacts.Query.Api.Configuration;

public static class ConsumersRegister
{
    public static void RegisterConsumers(this IServiceCollection services)
    {
        services.AddHostedService<ContactCreatedConsumer>();
        services.AddHostedService<ContactUpdatedConsumer>();
        services.AddHostedService<ContactDeletedConsumer>();
    }

}
