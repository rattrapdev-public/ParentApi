using Parent.Common;

namespace Parent.Domain;

public record ChildIdentifier : ValueObject
{
    public static ChildIdentifier CreateNew() => new ChildIdentifier(Guid.NewGuid());
    
    public Guid Id { get; private set; }

    public ChildIdentifier()
    {
    }

    public ChildIdentifier(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("The id cannot be empty");
        }
        
        Id = id;
    }
}