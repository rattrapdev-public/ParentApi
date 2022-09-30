using Parent.Domain;

namespace Parent.Application
{
    public interface IGetAllGuardians
    {
        Task<IEnumerable<Guardian>> HandleAsync();
    }
}