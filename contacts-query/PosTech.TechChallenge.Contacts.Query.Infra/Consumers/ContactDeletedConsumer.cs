using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using PosTech.TechChallenge.Contacts.Query.Domain.Entities;
using PosTech.TechChallenge.Contacts.Query.Infra.Context;
using PosTech.TechChallenge.Contacts.Query.Infra.Queues;
using PosTech.TechChallenge.Contacts.Query.Infra.Workers;

namespace PosTech.TechChallenge.Contacts.Query.Infra.Consumers;

public class ContactDeletedConsumer(ILogger<ConsumerWorker<Guid>> logger, IConfiguration configuration, IServiceScopeFactory scopeFactory) : ConsumerWorker<Guid>(logger, configuration)
{
    private readonly IServiceScopeFactory _scopeFactory = scopeFactory;
    protected override string QueueName => ContactQueues.ContactDeleted;

    protected override async Task OnMessageReceived(Guid id, CancellationToken cancellationToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ContactDbContext>();
        var dbSet = context.Set<Contact>();

        var contact = await dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken: cancellationToken);

        if (contact is not null)
        {
            dbSet.Remove(contact);
            await context.SaveChangesAsync(cancellationToken);
        }

        _logger.LogInformation("Deleted contact {id}", id);
    }
}