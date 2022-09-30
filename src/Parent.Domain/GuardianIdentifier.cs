namespace Parent.Domain;

public record GuardianIdentifier
{
    public Guid Id { get; }

    public GuardianIdentifier(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("The guid cannot be empty");
        }
        
        Id = id;
    }

    public GuardianIdentifier()
    {
        Id = Guid.NewGuid();
    }
}