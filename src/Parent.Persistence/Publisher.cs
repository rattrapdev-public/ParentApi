using MediatR;
using Parent.Domain;

namespace Parent.Persistence;

public class Publisher : IPublisher
{
    private readonly IMediator _mediator;

    public Publisher(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public async Task Publish(IEnumerable<IEntity> entities)
    {
        foreach (var entity in entities)
        {
            foreach (var evt in entity.DomainEvents)
            {
                await _mediator.Publish(evt);
            }
        }
    }
}