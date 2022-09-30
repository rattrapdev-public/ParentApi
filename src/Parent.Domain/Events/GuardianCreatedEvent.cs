using MediatR;

namespace Parent.Domain.Events;

public record GuardianCreatedEvent : IDomainEvent
{
    public GuardianIdentifier GuardianIdentifier { get; }
    public DateTime Occurred { get; }

    // Only identifier included as event is used internally
    public GuardianCreatedEvent(GuardianIdentifier guardianIdentifier)
    {
        GuardianIdentifier = guardianIdentifier ?? throw new ArgumentNullException(nameof(guardianIdentifier));
        Occurred = DateTime.UtcNow;
    }
}