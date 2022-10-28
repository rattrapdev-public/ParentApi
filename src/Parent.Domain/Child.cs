using Parent.Common;

namespace Parent.Domain;

public class Child : IEntity
{
    public static Child CreateNew(GuardianIdentifier guardianIdentifier, Name name)
    {
        return new Child(ChildIdentifier.CreateNew(), guardianIdentifier, name, ToyBox.Empty());
    }

    public static Child Reconstitute(ChildIdentifier childIdentifier, GuardianIdentifier guardianIdentifier, Name name,
        ToyBox toyBox)
    {
        return new Child(childIdentifier, guardianIdentifier, name, toyBox);
    }

    private readonly List<IDomainEvent> _domainList;

    public IEnumerable<IDomainEvent> DomainEvents => _domainList;
    
    public ChildIdentifier Identifier { get; private set; }
    public GuardianIdentifier GuardianIdentifier { get; private set; }
    
    public Name Name { get; private set; }

    public ToyBox ToyBox { get; private set; } = ToyBox.Empty();

    public Child()
    {
        _domainList = new List<IDomainEvent>();
    }

    protected Child(ChildIdentifier childIdentifier, GuardianIdentifier guardianIdentifier, Name name, ToyBox toyBox)
    {
        Identifier = childIdentifier;
        GuardianIdentifier = guardianIdentifier;
        Name = name;
        ToyBox = toyBox;

        _domainList = new List<IDomainEvent>();
    }

    public void AddToy(Toy toy)
    {
        ToyBox = ToyBox.AddToy(toy);
    }

    public void UpdateToy(Toy toy)
    {
        ToyBox = ToyBox.UpdateToys(toy);
    }
}