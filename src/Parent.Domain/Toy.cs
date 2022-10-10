using Parent.Common;

namespace Parent.Domain;

public record Toy : ValueObject
{
    public string Name { get; private set; }
    public string Upc { get; private set; }
    
    public Toy() {}

    public Toy(string upc, string name)
    {
        Upc = upc;
        Name = name;
    }
}