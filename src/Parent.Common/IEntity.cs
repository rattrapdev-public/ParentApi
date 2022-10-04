namespace Parent.Common;

public interface IEntity
{
    IEnumerable<IDomainEvent> DomainEvents { get; }
}