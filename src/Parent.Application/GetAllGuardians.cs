using Parent.Application.ViewModels;
using Parent.Domain;

namespace Parent.Application;

public class GetAllGuardians : IGetAllGuardians
{
    private readonly IGuardianRepository _guardianRepository;

    public GetAllGuardians(IGuardianRepository guardianRepository)
    {
        _guardianRepository = guardianRepository;
    }
    
    public async Task<IEnumerable<GuardianViewModel>> HandleAsync()
    {
        var guardians = await _guardianRepository.All();
        var viewModels = guardians.Select(x => new GuardianViewModel
        {
            GuardianId = x.Id,
            FirstName = x.Name.FirstName,
            LastName = x.Name.LastName,
            Email = x.Email.Email,
            Address1 = x.Address.Address1,
            Address2 = x.Address.Address2,
            City = x.Address.City,
            State = x.Address.State,
            Zip = x.Address.Zip,
        });

        return viewModels;
    }
}