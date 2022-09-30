using Parent.Application;
using Parent.Domain;

namespace Parent.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly GuardianDbContext _guardianDbContext;
    private readonly IPublisher _publisher;

    public UnitOfWork(GuardianDbContext guardianDbContext, IPublisher publisher)
    {
        _guardianDbContext = guardianDbContext;
        _publisher = publisher;
    }
    
    public async Task Commit()
    {
        var entitiesToPublish = _guardianDbContext.ChangeTracker.Entries<IEntity>().Select(x => x.Entity)
            .Where(x => x.DomainEvents.Any());

        await _guardianDbContext.SaveChangesAsync();

        await _publisher.Publish(entitiesToPublish);
    }
}