using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using PosTech.TechChallenge.Contacts.Query.Domain.Entities;
using PosTech.TechChallenge.Contacts.Query.Infra.Context;
using PosTech.TechChallenge.Contacts.Query.Infra.Queues;
using PosTech.TechChallenge.Contacts.Query.Infra.Workers;

namespace PosTech.TechChallenge.Contacts.Query.Infra.Consumers;

public class ContactCreatedConsumer(ILogger<ConsumerWorker<Contact>> logger, IServiceScopeFactory scopeFactory) : ConsumerWorker<Contact>(logger)
{
    private readonly IServiceScopeFactory _scopeFactory = scopeFactory;
    protected override string QueueName => ContactQueues.ContactCreated;

    protected override async Task OnMessageReceived(Contact contact, CancellationToken cancellationToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ContactDbContext>();
        var dbSet = context.Set<Contact>();

        dbSet.Add(contact);
        await context.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Added contact {id}", contact.Id);
    }
}
