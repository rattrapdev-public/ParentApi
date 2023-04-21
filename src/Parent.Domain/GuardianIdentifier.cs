using Parent.Common;

namespace Parent.Domain;

public record GuardianIdentifier : ValueObject
{
    public static GuardianIdentifier CreateNew()
    {
        return new GuardianIdentifier(Guid.NewGuid());
    }
    
    public Guid Id { get; private set; }

    public GuardianIdentifier(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("The guid cannot be empty");
        }
        
        Id = id;
    }
}