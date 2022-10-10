using Parent.Common;

namespace Parent.Domain;

public record Name : ValueObject
{
    public string FirstName { get; }
    public string LastName { get; }
    
    public Name() {}

    // ReSharper disable once ConvertToPrimaryConstructor
    public Name(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}