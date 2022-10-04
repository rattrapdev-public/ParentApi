using Parent.Common;

namespace Parent.Domain;

public record Toy : ValueObject
{
    public string Name { get; }
    public decimal Cost { get; }
    public string Upc { get; }

    public Toy(string upc, string name, decimal cost)
    {
        if (cost <= 0)
        {
            throw new ArgumentException("Cost cannot be less than 0");
        }

        Upc = upc;
        Name = name;
        Cost = cost;
    }
}