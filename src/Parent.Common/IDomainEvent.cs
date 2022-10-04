using MediatR;

namespace Parent.Common;

public interface IDomainEvent : INotification
{
    DateTime OccurredOn { get; }
}