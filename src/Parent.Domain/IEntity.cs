namespace Parent.Domain;

public interface IEntity
{ 
    IEnumerable<IDomainEvent> DomainEvents { get; }
}