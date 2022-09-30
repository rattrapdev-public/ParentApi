using Parent.Domain;

namespace Parent.Application;

public class CreateGuardian : ICreateGuardian
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGuardianRepository _guardianRepository;

    public CreateGuardian(IUnitOfWork unitOfWork, IGuardianRepository guardianRepository)
    {
        _unitOfWork = unitOfWork;
        _guardianRepository = guardianRepository;
    }


    public async Task HandleAsync(NewParentViewModel model)
    {
        var name = new Name(model.FirstName, model.LastName);
        var email = new EmailAddress(model.Email);
        var address = new Address(model.Address1, model.Address2, model.City, model.State, model.Zip);
        var parent = new Guardian(name, email, address);

        await _guardianRepository.Store(parent);
        await _unitOfWork.Commit();
    }
}