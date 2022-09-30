using MediatR;
using Parent.Domain.Events;

namespace Parent.Application;

public class GuardianCreatedConsoleService : INotificationHandler<GuardianCreatedEvent>
{
    public Task Handle(GuardianCreatedEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Received notification from event ${notification.GuardianIdentifier}");

        return Task.FromResult(0);
    }
}