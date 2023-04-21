using Parent.Common;

namespace Parent.Domain;

public record Name : SearchableValueObject
{
    public string FirstName { get; }
    public string LastName { get; }

    // ReSharper disable once ConvertToPrimaryConstructor
    public Name(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public override bool Contains(string searchText)
    {
        var normalizedSearchText = searchText.ToUpper();

        return FirstName.ToUpper().Contains(normalizedSearchText)
               || LastName.ToUpper().Contains(normalizedSearchText);
    }
}