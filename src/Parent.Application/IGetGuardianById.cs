using Parent.Application.ViewModels;

namespace Parent.Application;

public interface IGetGuardianById
{
    Task<GuardianViewModel> HandleAsync(Guid guardianId);
}