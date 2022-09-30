using Parent.Domain;

namespace Parent.Application;

public class GetAllGuardians : IGetAllGuardians
{
    private readonly IGuardianRepository _guardianRepository;

    public GetAllGuardians(IGuardianRepository guardianRepository)
    {
        _guardianRepository = guardianRepository;
    }
    
    public async Task<IEnumerable<Guardian>> HandleAsync()
    {
        return await _guardianRepository.All();
    }
}