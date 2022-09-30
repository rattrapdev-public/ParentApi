using MediatR;

namespace Parent.Domain;

public interface IDomainEvent : INotification
{
    public DateTime Occurred { get; }
}