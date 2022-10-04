using Parent.Application.ViewModels;

namespace Parent.Application;

public interface ICreateChild
{
    Task HandleAsync(NewChildViewModel viewModel);
}