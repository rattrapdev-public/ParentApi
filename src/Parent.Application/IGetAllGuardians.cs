using Parent.Application.ViewModels;
using Parent.Domain;

namespace Parent.Application
{
    public interface IGetAllGuardians
    {
        Task<IEnumerable<GuardianViewModel>> HandleAsync();
    }
}