using MediatR;

namespace Parent.Domain.Events;

public record GuardianMovedEvent : IDomainEvent
{
    public GuardianIdentifier GuardianIdentifier { get; }
    public Address Address { get; }
    public DateTime Occurred { get; }

    // Domain Event : Internal value object used to alert internal services of something that happened
    // Integration Event : Smaller message for message bus used to alert something has changed
    // Message : Full data included with an action expected, sent to message bus
    public GuardianMovedEvent(GuardianIdentifier guardianIdentifier)
    {
        GuardianIdentifier = guardianIdentifier;
        Occurred = DateTime.UtcNow;
    }
}