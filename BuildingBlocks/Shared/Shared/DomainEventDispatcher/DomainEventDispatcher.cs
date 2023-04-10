namespace BuildingBlocks.Subscriptions.Infrastructure;

using BuildingBlocks.Shared.DomainObjects;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

public class DomainEventDispatcher : SaveChangesInterceptor
{

    #region Members
    private readonly IPublisher _mediator;
    #endregion

    #region Ctor
    public DomainEventDispatcher(IPublisher mediator)
    {
        _mediator = mediator;
    }
    #endregion

    #region Method(s)
    public override InterceptionResult<int> SavingChanges(
       DbContextEventData eventData,
       InterceptionResult<int> result)
    {
        return SavingChangesAsync(eventData, result).GetAwaiter().GetResult();
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        await DispatchDomainEventsAsync(eventData.Context.ChangeTracker);
        return result;
    }

    private async Task DispatchDomainEventsAsync(ChangeTracker changeTracker)
    {
        var domainEntitiess = changeTracker
           .Entries<Entity>()
           .Select(x => x.Entity).ToList();

        var domainEntities = changeTracker
            .Entries<Entity>()
            .Select(x => x.Entity)
            .Where(x => x.DomainEvents != null && x.DomainEvents.Any())
            .ToList();

        foreach (var entity in domainEntities)
        {
            var events = entity.DomainEvents.ToArray();
            entity.ClearDomainEvents();
            foreach (var domainEvent in events)
            {
                await _mediator.Publish(domainEvent).ConfigureAwait(false);
            }
        }
    }
    #endregion
}