using Parent.Application.ViewModels;
using Parent.Domain;

namespace Parent.Application;

public class GetGuardianById : IGetGuardianById
{
    private readonly IGuardianRepository _guardianRepository;

    public GetGuardianById(IGuardianRepository guardianRepository)
    {
        _guardianRepository = guardianRepository;
    }
    
    public async Task<GuardianViewModel> HandleAsync(Guid guardianId)
    {
        var guardian = await _guardianRepository.GetBy(new GuardianIdentifier(guardianId));
        var model = new GuardianViewModel
        {
            GuardianId = guardian.Id,
            FirstName = guardian.Name.FirstName,
            LastName = guardian.Name.LastName,
            Email = guardian.Email.Email,
            Address1 = guardian.Address.Address1,
            Address2 = guardian.Address.Address2,
            City = guardian.Address.City,
            State = guardian.Address.State,
            Zip = guardian.Address.Zip,
        };

        return model;
    }
}