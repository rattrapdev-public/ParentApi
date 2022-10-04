using Parent.Application.ViewModels;

namespace Parent.Application;

public interface IGetGuardianById
{
    Task<ParentViewModel> HandleAsync(Guid guardianId);
}