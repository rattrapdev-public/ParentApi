using Parent.Application;
using Parent.Common;

namespace Parent.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly ParentDbContext _parentDbContext;
    private readonly IPublisher _publisher;

    public UnitOfWork(ParentDbContext parentDbContext, IPublisher publisher)
    {
        _parentDbContext = parentDbContext;
        _publisher = publisher;
    }
    
    public async Task Commit()
    {
        var entitiesToPublish = _parentDbContext.ChangeTracker.Entries<IEntity>().Select(x => x.Entity)
            .Where(x => x.DomainEvents.Any());

        await _parentDbContext.SaveChangesAsync();

        await _publisher.Publish(entitiesToPublish);
    }
}