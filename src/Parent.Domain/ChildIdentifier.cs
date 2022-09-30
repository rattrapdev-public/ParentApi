﻿namespace Parent.Domain;

public record ChildIdentifier
{
    public Guid Id { get; }

    public ChildIdentifier(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("The id cannot be empty");
        }
        
        Id = id;
    }

    public ChildIdentifier()
    {
        Id = Guid.NewGuid();
    }
}