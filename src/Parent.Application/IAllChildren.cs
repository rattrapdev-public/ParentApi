using Parent.Application.ViewModels;

namespace Parent.Application;

public interface IAllChildren
{
    Task<IEnumerable<ChildViewModel>> All();
}