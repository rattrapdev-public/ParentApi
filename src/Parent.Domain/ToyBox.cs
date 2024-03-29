﻿using Parent.Common;

namespace Parent.Domain;

public class ToyBox : ValueObjects<Toy>
{
    public static ToyBox Empty() => new ToyBox(Enumerable.Empty<Toy>());
    
    public ToyBox(IEnumerable<Toy> valueObjects) : base(valueObjects)
    {
    }

    public ToyBox AddToy(Toy toy)
    {
        var toyList = new List<Toy>(_valueList);
        toyList.Add(toy);
        return new ToyBox(toyList);
    }

    public ToyBox UpdateToys(Toy toy)
    {
        var toyList = new List<Toy>(_valueList);
        var toyToRemove = toyList.FirstOrDefault(t => t.Upc == toy.Upc);
        toyList.Remove(toyToRemove!);
        toyList.Add(toy);
        return new ToyBox(toyList);
    }
}