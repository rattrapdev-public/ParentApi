using Parent.Common;
using Parent.Domain.Events;

namespace Parent.Domain;

public class Guardian : IEntity
{
    public static Guardian CreateNew(Name name, EmailAddress emailAddress, Address address)
    {
        return new Guardian(new GuardianIdentifier(), name, emailAddress, address);
    }

    public static Guardian Reconstitute(GuardianIdentifier guardianIdentifier, Name name, EmailAddress emailAddress,
        Address address)
    {
        return new Guardian(guardianIdentifier, name, emailAddress, address);
    }
    
    private List<IDomainEvent> _domainEventList;

    public IEnumerable<IDomainEvent> DomainEvents => _domainEventList;

    public Guid Id => Identifier.Id;
    public GuardianIdentifier Identifier { get; }
    public Name Name { get; private set; }
    public EmailAddress Email { get; private set; }
    public Address Address { get; private set; }

    public Guardian(GuardianIdentifier guardianIdentifier, Name name, EmailAddress email, Address address)
    {
        _domainEventList = new List<IDomainEvent>();
        Identifier = guardianIdentifier;
        Name = name;
        Email = email;
        Address = address;
    }

    public void Move(Address address)
    {
        Address = address;
        _domainEventList.Add(new GuardianMovedEvent(Identifier));
    }
}