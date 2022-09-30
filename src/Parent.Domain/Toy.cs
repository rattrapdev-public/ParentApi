namespace Parent.Domain;

public record Toy
{
    public string Name { get; }
    public decimal Cost { get; }
    public int ToyId { get; }

    public Toy(int toyId, string name, decimal cost)
    {
        if (cost <= 0)
        {
            throw new ArgumentException("Cost cannot be less than 0");
        }

        ToyId = toyId;
        Name = name;
        Cost = cost;
    }
}