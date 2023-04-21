namespace Parent.Common;

public abstract record SearchableValueObject : ValueObject
{
    public abstract bool Contains(string searchText);
}