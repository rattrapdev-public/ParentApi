using Parent.Domain;

namespace Parent.Persistence;

public interface IPublisher
{
    Task Publish(IEnumerable<IEntity> entities);
}