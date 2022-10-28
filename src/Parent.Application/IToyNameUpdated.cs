using Parent.Application.MessageModels;

namespace Parent.Application;

public interface IToyNameUpdated
{
    Task HandleAsync(ToyNameChangedViewModel evt);
}